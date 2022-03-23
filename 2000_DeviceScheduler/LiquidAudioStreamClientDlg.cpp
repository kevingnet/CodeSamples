// LiquidAudioStreamClientDlg.cpp : implementation file
//

#include "stdafx.h"
#include "LiquidAudioStreamClient.h"
#include "LiquidAudioStreamClientDlg.h"
#include "DlgProxy.h"

#ifdef _DEBUG
#define new DEBUG_NEW
#undef THIS_FILE
static char THIS_FILE[] = __FILE__;
#endif

/////////////////////////////////////////////////////////////////////////////
// CLiquidAudioStreamClientDlg dialog

IMPLEMENT_DYNAMIC(CLiquidAudioStreamClientDlg, CDialog);

CLiquidAudioStreamClientDlg::CLiquidAudioStreamClientDlg(CWnd* pParent /*=NULL*/)
	: CDialog(CLiquidAudioStreamClientDlg::IDD, pParent)
{
	//{{AFX_DATA_INIT(CLiquidAudioStreamClientDlg)
	m_szFile = _T("1");
	//}}AFX_DATA_INIT
	// Note that LoadIcon does not require a subsequent DestroyIcon in Win32
	m_hIcon = AfxGetApp()->LoadIcon(IDR_MAINFRAME);
	m_pAutoProxy = NULL;
}

CLiquidAudioStreamClientDlg::~CLiquidAudioStreamClientDlg()
{
	// If there is an automation proxy for this dialog, set
	//  its back pointer to this dialog to NULL, so it knows
	//  the dialog has been deleted.
	if (m_pAutoProxy != NULL)
		m_pAutoProxy->m_pDialog = NULL;
}

void CLiquidAudioStreamClientDlg::DoDataExchange(CDataExchange* pDX)
{
	CDialog::DoDataExchange(pDX);
	//{{AFX_DATA_MAP(CLiquidAudioStreamClientDlg)
	DDX_Control(pDX, IDC_LIST_LOG, m_ctrlLogList);
	DDX_Control(pDX, IDC_STATIC_STATE, m_stState);
	DDX_Control(pDX, IDC_FILE, m_ctrlFile);
	DDX_Control(pDX, IDC_SPIN_FILE, m_SpinFile);
	DDX_Control(pDX, IDC_INFO, m_stInfo);
	DDX_Control(pDX, IDC_BTN_PLAY, m_btnPlay);
	DDX_Text(pDX, IDC_FILE, m_szFile);
	DDX_Control(pDX, IDC_STREAMER, m_Streamer);
	DDX_Control(pDX, IDC_SQL2XMLCTRL1, m_ctrlSQL2XML);
	//}}AFX_DATA_MAP
}

BEGIN_MESSAGE_MAP(CLiquidAudioStreamClientDlg, CDialog)
	//{{AFX_MSG_MAP(CLiquidAudioStreamClientDlg)
	ON_WM_PAINT()
	ON_WM_QUERYDRAGICON()
	ON_WM_CLOSE()
	ON_BN_CLICKED(IDC_BTN_PLAY, OnBtnPlay)
	ON_BN_CLICKED(IDC_STATIC_STATE, OnStaticState)
	ON_NOTIFY(UDN_DELTAPOS, IDC_SPIN_FILE, OnDeltaposSpinFile)
	ON_WM_LBUTTONDOWN()
	//}}AFX_MSG_MAP
END_MESSAGE_MAP()

/////////////////////////////////////////////////////////////////////////////
// CLiquidAudioStreamClientDlg message handlers

BOOL CLiquidAudioStreamClientDlg::OnInitDialog()
{
	CDialog::OnInitDialog();

	// Set the icon for this dialog.  The framework does this automatically
	//  when the application's main window is not a dialog
	SetIcon(m_hIcon, TRUE);			// Set big icon
	SetIcon(m_hIcon, FALSE);		// Set small icon
	
	// TODO: Add extra initialization here

	SetWindowText( m_DialogBoxTitle );

	g_pLog->SetLogListCtrl( &m_ctrlLogList );
	g_pLog->Initialize();

	m_bmpPlay.LoadBitmap( IDB_PLAY );
	m_bmpStop.LoadBitmap( IDB_STOP );

	m_btnPlay.SetBitmap( (HBITMAP)m_bmpPlay );

	m_SpinFile.SetRange( 1, UD_MAXVAL  );
	m_SpinFile.SetRange32( 1, UD_MAXVAL  );
	m_SpinFile.SetPos( 1 );
	m_SpinFile.SetBuddy( &m_ctrlFile );

	//skip the leading " character
	CString szCmdLine = (LPCTSTR)(GetCommandLine()+1);

	m_Session.SetDialog( this );
	m_Session.SetStreamer( &m_Streamer );
	m_Session.Initialize();

	UpdateUI();
	UpdateInfo();

	return TRUE;  // return TRUE  unless you set the focus to a control
}

// If you add a minimize button to your dialog, you will need the code below
//  to draw the icon.  For MFC applications using the document/view model,
//  this is automatically done for you by the framework.

void CLiquidAudioStreamClientDlg::OnPaint() 
{
	if (IsIconic())
	{
		CPaintDC dc(this); // device context for painting

		SendMessage(WM_ICONERASEBKGND, (WPARAM) dc.GetSafeHdc(), 0);

		// Center icon in client rectangle
		int cxIcon = GetSystemMetrics(SM_CXICON);
		int cyIcon = GetSystemMetrics(SM_CYICON);
		CRect rect;
		GetClientRect(&rect);
		int x = (rect.Width() - cxIcon + 1) / 2;
		int y = (rect.Height() - cyIcon + 1) / 2;

		// Draw the icon
		dc.DrawIcon(x, y, m_hIcon);
	}
	else
	{
		CDialog::OnPaint();
	}
}

// The system calls this to obtain the cursor to display while the user drags
//  the minimized window.
HCURSOR CLiquidAudioStreamClientDlg::OnQueryDragIcon()
{
	return (HCURSOR) m_hIcon;
}

// Automation servers should not exit when a user closes the UI
//  if a controller still holds on to one of its objects.  These
//  message handlers make sure that if the proxy is still in use,
//  then the UI is hidden but the dialog remains around if it
//  is dismissed.

void CLiquidAudioStreamClientDlg::OnClose() 
{
	if (CanExit())
		CDialog::OnClose();
}

void CLiquidAudioStreamClientDlg::OnOK() 
{
	if (CanExit())
		CDialog::OnOK();
}

void CLiquidAudioStreamClientDlg::OnCancel() 
{
	if (CanExit())
		CDialog::OnCancel();
}

BOOL CLiquidAudioStreamClientDlg::CanExit()
{
	// If the proxy object is still around, then the automation
	//  controller is still holding on to this application.  Leave
	//  the dialog around, but hide its UI.
	if (m_pAutoProxy != NULL)
	{
		ShowWindow(SW_HIDE);
		return FALSE;
	}

	return TRUE;
}

BEGIN_EVENTSINK_MAP(CLiquidAudioStreamClientDlg, CDialog)
    //{{AFX_EVENTSINK_MAP(CLiquidAudioStreamClientDlg)
	ON_EVENT(CLiquidAudioStreamClientDlg, IDC_STREAMER, 1 /* StreamerError */, OnStreamerErrorStreamer, VTS_I4)
	ON_EVENT(CLiquidAudioStreamClientDlg, IDC_SQL2XMLCTRL1, -608 /* Error */, OnErrorSql2xmlctrl1, VTS_I2 VTS_PBSTR VTS_SCODE VTS_BSTR VTS_BSTR VTS_I4 VTS_PBOOL)
	ON_EVENT(CLiquidAudioStreamClientDlg, IDC_SQL2XMLCTRL1, 1 /* QueryComplete */, OnQueryCompleteSql2xmlctrl1, VTS_NONE)
	//}}AFX_EVENTSINK_MAP
END_EVENTSINK_MAP()

void CLiquidAudioStreamClientDlg::ResetError()
{
	m_szError.Empty();
}

void CLiquidAudioStreamClientDlg::OnErrorSql2xmlctrl1(short Number, BSTR FAR* Description, SCODE Scode, LPCTSTR Source, LPCTSTR HelpFile, long HelpContext, BOOL FAR* CancelDisplay) 
{
	m_szError = ((char*) ((_bstr_t)*Description));
	m_stState.SetWindowText( m_szError );
	m_Session.m_Server.Send( (PBYTE)(LPCTSTR)m_szError, m_szError.GetLength() );
	g_pLog->Log( m_szError, LOGTYPE_ERROR );
}

void CLiquidAudioStreamClientDlg::OnStreamerErrorStreamer(long errorCode) 
{
	m_Session.SetError( true );
	if( errorCode == ERROR_INVALID_HANDLE )
	{
		//typical when the file is not found, display better info
		m_szError = "file id not found";
	}
	else
	{
		HRESULT hr = MAKE_HRESULT( SEVERITY_ERROR, FACILITY_WIN32, errorCode );
		get_system_error_string ( m_szError, hr );
	}
	m_stState.SetWindowText( m_szError );
	m_Session.m_Server.Send( (PBYTE)(LPCTSTR)m_szError, m_szError.GetLength() );
	//reenable
	m_btnPlay.EnableWindow( TRUE );
	g_pLog->Log( m_szError, LOGTYPE_ERROR );
}

void CLiquidAudioStreamClientDlg::OnBtnPlay()
{
	//disable temporarily
	m_btnPlay.EnableWindow( FALSE );
	ResetError();
	UpdateData();
	m_ctrlFile.SetFocus();
	if( m_Session.GetState() == SESSION_STATE_STOPPED )
	{
		g_pLog->Log( "UI request: play song" );
		m_Session.Play( m_szFile );
	}
	else
	{
		g_pLog->Log( "UI request: stop music" );
		m_Session.Stop();
	}
}

void CLiquidAudioStreamClientDlg::UpdateInfo()
{
	CString szInfo;
	szInfo.Format( "conn(s): {%i} port: {%i} url: {%s} ", m_Session.GetConnectionCount(), m_Session.GetListenPort(), m_Session.GetURL() );
	m_stInfo.SetWindowText( szInfo );
}

void CLiquidAudioStreamClientDlg::UpdateUI()
{
	switch( m_Session.GetState() )
	{
	case SESSION_STATE_PLAYING:
		m_SpinFile.SetPos( atoi(m_szFile) );
		m_SpinFile.EnableWindow( FALSE );
		m_ctrlFile.EnableWindow( FALSE );
		m_btnPlay.SetBitmap( (HBITMAP)m_bmpStop );
		m_stState.SetWindowText( "playing..." );
		break;
	case SESSION_STATE_STOPPED:
		m_SpinFile.EnableWindow( TRUE );
		m_ctrlFile.EnableWindow( TRUE );
		m_btnPlay.SetBitmap( (HBITMAP)m_bmpPlay );
		m_stState.SetWindowText( "select file to play" );
		break;
	}
	//reenable
	m_btnPlay.EnableWindow( TRUE );
}

void get_system_error_string ( CString& a_szError, DWORD a_dwError )
{
	LPSTR	pText = NULL;
	if ( 0 != ::FormatMessage( FORMAT_MESSAGE_ALLOCATE_BUFFER |
							   FORMAT_MESSAGE_FROM_SYSTEM | 
							   FORMAT_MESSAGE_IGNORE_INSERTS, 
							   NULL,
							   a_dwError, 
							   MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
							   (LPTSTR)&pText, 0, NULL ) )
	{
		if ( pText )
		{
			a_szError += pText;
			LocalFree( pText );
		}
	}
}

void CLiquidAudioStreamClientDlg::OnLButtonDown(UINT nFlags, CPoint point) 
{
	UpdateData();
	Buy( "000111222", m_szFile );
	CDialog::OnLButtonDown(nFlags, point);
}

void CLiquidAudioStreamClientDlg::Buy( LPCTSTR szUserID, LPCTSTR szSongID )
{
	if( !szUserID || !*szUserID || !szSongID ||!*szSongID )
	{
		m_szError = "Could not buy song, invalid parameter(s)";
		m_stState.SetWindowText( m_szError );
		g_pLog->Log( m_szError, LOGTYPE_ERROR );
	}
	else
	{
		CString szSQL;
		szSQL.Format( m_szSQLString, szUserID, szSongID );
		m_ctrlSQL2XML.Execute( m_szDSN, szSQL, NULL );
		g_pLog->Log( "Network request: Purchase song" );
	}
}

void CLiquidAudioStreamClientDlg::OnStaticState() 
{
	UpdateData();
}

void CLiquidAudioStreamClientDlg::OnQueryCompleteSql2xmlctrl1() 
{
	g_pLog->Log( "Purchase song request completed" );
}

void CLiquidAudioStreamClientDlg::OnDeltaposSpinFile(NMHDR* pNMHDR, LRESULT* pResult) 
{
	NM_UPDOWN* pNMUpDown = (NM_UPDOWN*)pNMHDR;
	m_stState.SetWindowText( "click on button to play song" );

	*pResult = 0;
}

