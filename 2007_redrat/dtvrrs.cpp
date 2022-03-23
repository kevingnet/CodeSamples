#include "dtvrrs.h"

using namespace std;

Logger * pLogger = 0;
Config * pConfig = 0;
bool debug = false;

int main (int argc, char* argv[])
{
	try {
		Config 		cfg;
		pConfig 	= &cfg;
		ConfigMap * 	pcm = new ConfigMap();

		Logger 		logger(pConfig->GetLogFilePath());
		pLogger 	= &logger;
		Socket::SetLogger(pLogger);

		pConfig->MergeConfigFiles(*pcm);
		pConfig->MergeCommandLineOptions(argc, argv);

		debug = pConfig->IsDebug();

		if(debug){
			pcm->Dump();
			pConfig->Dump();
			cout << "\nRunning interactive mode. Press the letter 'h' and the [ENTER] key for available commands.\n\n";
		}
		delete pcm;

		SocketManager server;

		initialize_app();

		server.Initialize(pConfig->GetPort(), pConfig->GetMaxClients(), pConfig->GetTimeOut());
		server.StartListening();
		
		while (1) 
		{
			server.Poll();
			if(debug)
				process_input();
		}

	} catch (SocketException& e) {
		pLogger->Output("ERROR!: Socket exception %s", e.what() );
	} catch (ConfigException& e) {
		pLogger->Output("ERROR!: Config exception %s", e.what() );
	} catch (ProcessException& e) {
		pLogger->Output("ERROR!: Process exception %s", e.what() );
	} catch (exception& e) {
		pLogger->Output("ERROR!: exception %s", e.what() );
	} catch (...) {
		pLogger->Output("ERROR!: unknown exception %s", strerror(errno));
	}
	return 0;
}

void process_input()
{
	int k = kbhit();
        if ( k != 0 )
	{
       		int c = fgetc(stdin);
		switch(c){
		case 'd':
			pConfig->Dump();
			break;
		case 'q':
			exit(0);
		case 'b':
			debug = false;
			daemonize();
			break;
		case 'h':
			cout << "\nAvailable commands:\n d: display status info, b: run in the background, q: exit\n\n";
			break;
		}
	}
}

void initialize_app(void)
{
	chmod( pConfig->GetLogFilePath(), S_IROTH | S_IRUSR | S_IWUSR | S_IRGRP );
	pLogger->Output("\n\n--------------------------------------------------------------------------------------\nInitializing...\n");

	//check if process is already running...
	struct stat fs;
	if( 0 == stat(pConfig->GetRunFilePath(), &fs) ){
		char buf[MAX_TEXT];
		ifstream runfile;
		runfile.open (pConfig->GetRunFilePath(), ifstream::in);
		if (runfile.good()){
			buf[LINE_LEN-1] = 0;
			runfile.getline(buf,LINE_LEN);
		}
		runfile.close();

		char procpath[MAX_TEXT];
		snprintf(procpath, MAX_TEXT,"/proc/%d/exe",atoi(buf));
		if( readlink(procpath,buf,MAX_TEXT) > 0 ){ 
			throw ProcessException("Error: Process already running in the background. Please shutdown running server first.");
		}
		pLogger->Output("Warning: Found unused process run file. This may indicate improper server shutdown.");
		remove(pConfig->GetRunFilePath());
	}

	if(!pConfig->IsDebug())	{
		daemonize();
	}else{
		setup_signals();
	}
}

void shutdown_app(void)
{
	pLogger->Output("\nShutting down...\n~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~\n\n");
	remove(pConfig->GetRunFilePath());
}

void daemonize()
{
	pLogger->Output("Running as a background process...");
	pLogger->DeleteFlag(Logger::MODE_PRINT);
	int i,lfp;
	char str[10];
	if(getppid()==1) return; /* already a daemon */
	i=fork();
	if (i<0) exit(1); /* fork error */
	if (i>0) exit(0); /* parent exits */
	/* child (daemon) continues */
	setsid(); /* obtain a new process group */
	for (i=getdtablesize();i>=0;--i) close(i); /* close all descriptors */
	i=open("/dev/null",O_RDWR); dup(i); dup(i); /* handle standart I/O */
	umask(027); /* set newly created file permissions */
	chdir(pConfig->GetWorkingDir()); /* change running directory */
	lfp=open(pConfig->GetRunFilePath(),O_RDWR|O_CREAT,0640);
	if (lfp<0) exit(1); /* can not open */
	if (lockf(lfp,F_TLOCK,0)<0) exit(0); /* can not lock */
	/* first instance continues */
	sprintf(str,"%d\n",getpid());
	write(lfp,str,strlen(str)); /* record pid to lockfile */
	setup_signals();
	signal(SIGCHLD,SIG_IGN); /* ignore child */
	signal(SIGTSTP,SIG_IGN); /* ignore tty signals */
	signal(SIGTTOU,SIG_IGN);
	signal(SIGTTIN,SIG_IGN);
}

void setup_signals()
{
	signal(SIGHUP,signal_handler); /* catch hangup signal */
	signal(SIGTERM,signal_handler); /* catch kill signal */
	signal(SIGQUIT,signal_handler); 
	signal(SIGABRT,signal_handler); 
	signal(SIGSTOP,signal_handler); 
	signal(SIGTSTP,signal_handler); 
	signal(SIGCONT,signal_handler); 
	signal(SIGUSR1,signal_handler); 
	signal(SIGUSR2,signal_handler); 
}

void signal_handler(int sig)
{
	bool shutdown = false;
	switch(sig) {
	case SIGUSR1:
		pLogger->Output("SIGUSR1 signal caught");
		break;
	case SIGUSR2:
		pLogger->Output("SIGUSR2 signal caught");
		break;
	case SIGHUP:
		pLogger->Output("SIGHUP signal caught");
		break;

	case SIGSTOP:
		pLogger->Output("SIGSTOP signal caught");
		break;
	case SIGTSTP:
		pLogger->Output("SIGTSTP signal caught");
		break;
	case SIGCONT:
		pLogger->Output("SIGCONT signal caught");
		break;

	case SIGTERM:
		pLogger->Output("SIGTERM signal caught");
		shutdown = true;
		break;
	case SIGQUIT:
		pLogger->Output("SIGQUIT signal caught");
		shutdown = true;
		break;
	case SIGABRT:
		pLogger->Output("SIGABRT signal caught");
		shutdown = true;
		break;
	}
	if( shutdown ){
		shutdown_app();
		exit(EXIT_SUCCESS);
	}
}

int kbhit()
{
    struct timeval tv;
    fd_set fds;
    tv.tv_sec = 0;
    tv.tv_usec = 0;
    FD_ZERO(&fds);
    FD_SET(STDIN_FILENO, &fds); //STDIN_FILENO is 0
    select(STDIN_FILENO+1, &fds, NULL, NULL, &tv);
    return FD_ISSET(STDIN_FILENO, &fds);
}

/*
SIGHUP  	1  	Exit  	Hangup
SIGINT 		2 	Exit 	Interrupt
SIGKILL 	9 	Exit 	Killed
SIGPIPE 	13 	Exit 	Broken Pipe
SIGALRM 	14 	Exit 	Alarm Clock
SIGTERM 	15 	Exit 	Terminated
SIGUSR1 	16 	Exit 	User Signal 1
SIGUSR2 	17 	Exit 	User Signal 2
SIGVTALRM 	28 	Exit 	Virtual Timer Expired
SIGPROF 	29 	Exit 	Profiling Timer Expired
SIGQUIT 	3 	Core 	Quit
SIGILL 		4 	Core 	Illegal Instruction
SIGTRAP 	5 	Core 	Trace/Breakpoint Trap
SIGABRT 	6 	Core 	Abort
SIGEMT 		7 	Core 	Emulation Trap
SIGFPE 		8 	Core 	Arithmetic Exception
SIGBUS 		10 	Core 	Bus Error
SIGSEGV 	11 	Core 	Segmentation Fault
SIGSYS 		12 	Core 	Bad System Call
SIGXCPU 	30 	Core 	CPU time limit exceeded
SIGXFSZ 	31 	Core 	File size limit exceeded
SIGCHLD 	18 	Ignore 	Child Status
SIGPWR 		19 	Ignore 	Power Fail/Restart
SIGWINCH 	20 	Ignore 	Window Size Change
SIGURG 		21 	Ignore 	Urgent Socket Condition
SIGPOLL 	22 	Ignore 	Socket I/O Possible
SIGCONT 	25 	Ignore 	Continued
SIGWAITING 	32 	Ignore 	All LWPs blocked
SIGLWP 		33 	Ignore 	Virtual Interprocessor Interrupt for Threads Library
SIGAIO 		34 	Ignore 	Asynchronous I/O
SIGSTOP 	23 	Stop 	Stopped (signal)
SIGTSTP 	24 	Stop 	Stopped (user)
SIGTTIN 	26 	Stop 	Stopped (tty input)
SIGTTOU 	27 	Stop 	Stopped (tty output)
*/


