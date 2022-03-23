#include "stdafx.h"
#include "resource.h"
#include "CommLib.h"

void CopyData( CByteArray& pB, const PBYTE pData, int iSize )
{
	pB.SetSize( iSize + 1 );
	strncpy( (char*)&(pB[0]), (LPCTSTR)pData, iSize );
}

void CopyData( CByteArray& pB, int idx, const PBYTE pData, int iSize )
{
	pB.SetSize( iSize + idx + 1 );
	strncpy( (char*)&(pB[idx]), (LPCTSTR)pData, iSize );
}


void AppendData( CByteArray& pB, const PBYTE pData, int iSize )
{
	int iArraySize = pB.GetSize();
	pB.SetSize( iArraySize + iSize );
	for( int i=0; i<iSize; i++ )
	{
		pB.SetAt( iArraySize++, pData[i] );
	}
}

///////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////
// TCPCOMMPORT
///////////////////////////////////////////////////////////////////////////////////////

TCPSocket::TCPSocket()
{
	SetState( IP_STATE_CLOSED );
	m_uiPort = 2100;
	m_iMaxBufferSize = MAX_BUFFER_SIZE;
}

TCPSocket::~TCPSocket()
{
	BYTE Buffer[50];
	ShutDown();
	while(Receive(Buffer,50) > 0);
	CAsyncSocket::Close();
}

TCPSocket& TCPSocket::operator=( const TCPSocket& pW )
{
	m_State		= pW.m_State;
	m_szAddress	= pW.m_szAddress;
	m_uiPort	= pW.m_uiPort;

	return *this;
}

void TCPSocket::SetDefaultOptions( void ) 
{
	BOOL bOpt = TRUE;

	if( !SetSockOpt( SO_DONTLINGER, &bOpt, sizeof( bOpt ))) 
	{
		DisplayLastError( GetLastError() );
	}

	if( !SetSockOpt( SO_KEEPALIVE, &bOpt, sizeof( bOpt ))) 
	{
		DisplayLastError( GetLastError() );
	}
}

void TCPSocket::OpenListenPort( UINT uiPort )
{
	// Attempts to connect to the device on the requested 
	// address and port.
	ASSERT( uiPort );

	if( uiPort == 0 ) return;

	int	iLastError = 0;

	// The port is already initialized.
	switch( GetState() ) 
	{
		case IP_STATE_CONNECTED: 
		case IP_STATE_CONNECTING: 
		case IP_STATE_LISTENING: 
			Close();
			break;
		case IP_STATE_DISCONNECTING: 
		case IP_STATE_CREATED: 
		case IP_STATE_CLOSED: 
			break;
	}

	m_uiPort = uiPort;

	if( iLastError == 0 )
	{
		if( IsState( IP_STATE_CLOSED ) )
		{
			// The port has not been initialized yet.
			if( Create( m_uiPort ) ) 
			{
				SetState( IP_STATE_CREATED );
			} 
			else 
			{
				iLastError = GetLastError();
			}
		}
	}

	if( iLastError == 0 )
	{
		if( Listen() )
		{
			SetState( IP_STATE_LISTENING );
		} 
		else 
		{
			iLastError = GetLastError();
			if( iLastError == WSAEWOULDBLOCK ) 
			{
				iLastError = 0;
				SetState( IP_STATE_LISTENING );
			}
		}
	}

	if( iLastError != 0 ) 
	{
		DisplayLastError( iLastError );
	}
}


void TCPSocket::OpenPort( LPCTSTR pAddress, UINT uiPort )
{
	// Attempts to connect to the device on the requested 
	// address and port.
	ASSERT( uiPort );
	ASSERT( pAddress );
	ASSERT( *pAddress );

	int		iLastError = 0;

	// The port is already initialized.
	switch( GetState() ) 
	{
		case IP_STATE_CONNECTED: 
		case IP_STATE_CONNECTING:
			// The socket is already talking to a device.
			// If the connection is to a different port 
			// close it down.
			if( m_szAddress == pAddress && m_uiPort == uiPort )
				return;
			Close();
			break;
		case IP_STATE_LISTENING: 
			Close();
			break;
		case IP_STATE_DISCONNECTING: 
		case IP_STATE_CREATED: 
		case IP_STATE_CLOSED: 
			break;
	}

	m_szAddress		= pAddress;
	m_uiPort		= uiPort;

	if( iLastError == 0 )
	{
		if( IsState( IP_STATE_CLOSED ) )
		{
			// The port has not been initialized yet.
			if( Create() ) 
			{
				SetState( IP_STATE_CREATED );
			} 
			else 
			{
				iLastError = GetLastError();
			}
		}
	}

	if( iLastError == 0 ) 
	{
		if( Connect( m_szAddress, m_uiPort ) )
		{
			// The socket has connected. 
			SetState( IP_STATE_CONNECTED );
		} 
		else 
		{
			iLastError = GetLastError();
			if( iLastError == WSAEWOULDBLOCK ) 
			{
				iLastError = 0;
				SetState( IP_STATE_CONNECTING );
			} 
		}
	}

	if( iLastError != 0 ) 
	{
		DisplayLastError( iLastError );
	}
}


void TCPSocket::OpenPort( void )
{
	int		iLastError = 0;

	if(		m_uiPort < 0 
		||	m_uiPort > 65535
		||	!m_szAddress.GetLength()
	) 
	{
		return;
	}

	switch( GetState() ) 
	{
		case IP_STATE_CONNECTED: 
		case IP_STATE_CONNECTING:
			return;
			break;
		case IP_STATE_LISTENING: 
			Close();
			break;
		case IP_STATE_DISCONNECTING: 
		case IP_STATE_CREATED: 
		case IP_STATE_CLOSED: 
			break;
	}

	if( iLastError == 0 )
	{
		if( IsState( IP_STATE_CLOSED ) )
		{
			// The port has not been initialized yet.
			if( Create() ) 
			{
				SetState( IP_STATE_CREATED );
			} 
			else 
			{
				iLastError = GetLastError();
			}
		}
	}

	if( iLastError == 0 )
	{
		if( IsState( IP_STATE_CREATED ) )
		{
			if( Connect( m_szAddress, m_uiPort )) 
			{
				// The socket has connected. 
				SetState( IP_STATE_CONNECTED );
			} 
			else 
			{
				iLastError = GetLastError();
				if( iLastError == WSAEWOULDBLOCK ) 
				{
					SetState( IP_STATE_CONNECTING );
				} 
			}
		}
	}

	if( iLastError != 0 ) 
	{
		DisplayLastError( iLastError );
	}
}

void TCPSocket::Close( void )
{
	BYTE Buffer[50];
	ShutDown();
	while(Receive(Buffer,50) > 0);
	CAsyncSocket::Close();
	SetState( IP_STATE_CLOSED );
}

BOOL TCPSocket::Send( PBYTE pData, int iSize )
{
	if( !IsOpen() ) 
	{
		return FALSE;
	}

	int		iLastError = 0;
	int iCount = CAsyncSocket::Send( pData, iSize );
	if( iCount == SOCKET_ERROR )
	{
		iLastError = GetLastError();
		if( iLastError == WSAEWOULDBLOCK ) 
		{
			if( m_baBuffer.GetSize() > m_iMaxBufferSize )
			{
				DisplayLastError( IDS_ERR_BUFFER_FULL );
				return FALSE;
			}
			//copy all to buffer
			AppendData( m_baBuffer, pData, iSize );
		}
		else
		{
			DisplayLastError( iLastError );
			return FALSE;
		}
	}
	else if( iCount != iSize ) 
	{
		if( m_baBuffer.GetSize() > m_iMaxBufferSize )
		{
			DisplayLastError( IDS_ERR_BUFFER_FULL2 );
			return FALSE;
		}
		//copy left over to buffer
		AppendData( m_baBuffer, pData+iCount, iSize-iCount );
	}
	return TRUE;
}

BOOL TCPSocket::SendOutOfBandData( PBYTE pData, int iSize )
{
	if( !IsOpen() ) 
	{
		return FALSE;
	}

	int		iLastError = 0;
	int iCount = CAsyncSocket::Send( pData, iSize, MSG_OOB );
	if( iCount == SOCKET_ERROR )
	{
		iLastError = GetLastError();
		if( iLastError == WSAEWOULDBLOCK ) 
		{
			if( m_baBuffer.GetSize() > m_iMaxBufferSize )
			{
				DisplayLastError( IDS_ERR_BUFFER_FULL );
				return FALSE;
			}
			//copy all to buffer
			AppendData( m_baBuffer, pData, iSize );
		}
		else
		{
			DisplayLastError( iLastError );
			return FALSE;
		}
	}
	else if( iCount != iSize ) 
	{
		if( m_baBuffer.GetSize() > m_iMaxBufferSize )
		{
			DisplayLastError( IDS_ERR_BUFFER_FULL2 );
			return FALSE;
		}
		//copy left over to buffer
		AppendData( m_baBuffer, pData+iCount, iSize-iCount );
	}
	return TRUE;
}

void TCPSocket::OnAccept( int nErrorCode )
{
	switch( nErrorCode ) 
	{
		case 0:
			OnClientConnected();
			break;
		default:
			DisplayLastError( nErrorCode );
	}
}

void TCPSocket::OnConnect( int nErrorCode )
{
	// Notification that the connection has completed.
	// Check for errors and mark the port as open if success.
	switch( nErrorCode ) 
	{
		case 0:
			SetState( IP_STATE_CONNECTED );
			break;
		default:
			Close();
			DisplayLastError( nErrorCode );
	}
}

void TCPSocket::OnOutOfBandData( int nErrorCode )
{
	// called when a connection occurs. Don't do anything
}

void TCPSocket::OnSend( int nErrorCode )
{
	if( nErrorCode )
		DisplayLastError( nErrorCode );

	int		iLastError = 0;
	m_iBytesSent = 0;
	m_iBufferSize = m_baBuffer.GetSize();

	while( m_iBytesSent < m_iBufferSize )
	{      
		int dwBytes = Send( m_baBuffer.GetData() + m_iBytesSent, m_iBufferSize - m_iBytesSent );
		if( dwBytes == SOCKET_ERROR )
		{
			iLastError = GetLastError();
			if( iLastError == WSAEWOULDBLOCK )
				break;
			else
				DisplayLastError( iLastError );
		}
		else
		{
			m_iBytesSent += dwBytes;
		}
	}
	if( m_iBytesSent == m_iBufferSize )
	{
		m_iBytesSent = m_iBufferSize = 0;
		m_baBuffer.RemoveAll();
	}
	CAsyncSocket::OnSend(nErrorCode);
}

void TCPSocket::OnReceive( int nErrorCode )
{
	// Notification for received data. Default
	// implementation just flushes the buffer.
	BYTE Data[5];
	Receive(Data,5);

	CString szError;

	szError.Format( "%2x %2x %2x %2x %2x", 
		Data[0],Data[1],Data[2],Data[3],Data[4]
	);

	AfxMessageBox( szError );
	CWinSocket::OnReceive( nErrorCode );
}

void TCPSocket::OnClose( int nErrorCode )
{
	// Notification that the Socket has closed.
	// This means returning the state to not connected.
	switch( nErrorCode ) 
	{
		case 0:
			Close();
			break;
		default:
			DisplayLastError( nErrorCode );
			Close();
			break;
	}
}

void TCPSocket::OnClientConnected( void )
{
	// called when a connection occurs. Don't do anything
}

void TCPSocket::GetAddress( CString& szAddress, UINT& uiPort )
{
	szAddress	= m_szAddress;
	uiPort		= m_uiPort;
}

void TCPSocket::SetAddress( CString& szAddress, UINT uiPort )
{
	m_szAddress	= szAddress;
	m_uiPort	= uiPort;
}

BOOL TCPSocket::IsOpen( void )
{
	return m_State == IP_STATE_CONNECTED;
}

TCPSocket::SocketState TCPSocket::GetState( void )
{
	return m_State;
}

void TCPSocket::SetState( SocketState State ) 
{ 
	m_State = State;

	if( m_State == IP_STATE_CREATED )
	{
		SetDefaultOptions();
	}
	OnStateChanged( State );
}

void TCPSocket::OnStateChanged( SocketState State )
{
	// notification to derived classes that
	// something has happened.
}

LPCTSTR	TCPSocket::GetStateString()
{
	for( int i=0; i<IP_STATE_MAX; i++ ) 
	{
		if( m_StatesTable[i].State == m_State ) 
		{
			return m_StatesTable[i].Text;
		}
	}
	return m_StatesTable[0].Text;
}

//////////////////////////////////////////////////////////////////////////////////////////////
// MANAGER PORT
//////////////////////////////////////////////////////////////////////////////////////////////


TCPServerSocket::TCPServerSocket()
{
	m_pManager = NULL;
}

TCPServerSocket::~TCPServerSocket()
{
}

void TCPServerSocket::Close( void )
{
	TCPSocket::Close();

	// notify the manager class that we are closing.
	if( m_pManager ) {
		m_pManager->OnPortClosed( this );
	}
}

void TCPServerSocket::SetConnectedState( void )
{
	SetState( IP_STATE_CONNECTED );
}

void TCPServerSocket::OnConnectionMade( void )
{
	// called when the connection is completely established
}

void TCPServerSocket::OnReceive( int nErrorCode )
{
	BYTE	Data[50];
	BOOL	More = TRUE;

	while( More ) 
	{
		int iCount = Receive( Data, sizeof(Data) );

		if( iCount == SOCKET_ERROR ) 
		{
			int iError = GetLastError();
			if( iError && iError != WSAEWOULDBLOCK ) 
			{
				DisplayLastError( iError );
			}
			More = FALSE;
		}
		else 
		{
			ProcessReceivedPacket( Data, iCount );
		}
	}
}

void TCPServerSocket::ProcessReceivedPacket( PBYTE pData, int iCount )
{
}


//////////////////////////////////////////////////////////////////////////////////////////////
// MANAGER PORT
//////////////////////////////////////////////////////////////////////////////////////////////


TCPListenManagerSocket::TCPListenManagerSocket()
{
}

TCPListenManagerSocket::~TCPListenManagerSocket()
{
	Close();
}

void TCPListenManagerSocket::Close( void )
{
	for( int i=0; i<m_Clients.GetSize(); i++ ) 
	{
		delete m_Clients[i];
	}
	m_Clients.RemoveAll();
	TCPSocket::Close();
}
 
void TCPListenManagerSocket::OnClientConnected( void )
{
	bool More = true;

	while( More ) 
	{
		TCPServerSocket*	pClient			= NULL;

		// search for an empty server object.

		for( int i = 0; (pClient == NULL) && (i < m_Clients.GetSize()); i++ ) 
		{
			if( m_Clients[i]->GetState() == IP_STATE_CLOSED ) 
			{
				pClient = m_Clients[i];
				ReuseServer( pClient );
			}
		}

		if( !pClient ) 
		{
			pClient = CreateServer();
			m_Clients.Add( pClient );
		}

		if( pClient ) 
		{
			pClient->SetManager( this );

			if( Accept( *pClient ) == 0 ) 
			{
				// we need to remove it from the client list
				for( int i = 0; i < m_Clients.GetSize(); i++ ) 
				{
					if( m_Clients[i] == pClient ) 
					{
						m_Clients.RemoveAt( i );
					}
				}
				
				delete pClient;	
				More = false;
			} 
			else 
			{
				// let the derived class do things when connection established.
				pClient->SetConnectedState();
				pClient->OnConnectionMade();
				OnClientListChanged();
			}
		}
	}
}

void TCPListenManagerSocket::OnPortClosed( TCPServerSocket* pPort )
{
	// this function is called by a TCPServer port when it shuts down.
	// This presents a bit of a problem because we can't delete the object
	// because it is still alive at this point. Instead this implementation
	// marks the object as available so that next time a connection is made
	// we simply reuse any closed socket objects. If none are available then
	// we create a new one.
	// 
	// To achieve the above action actually requires no work since the test for
	// closed comes completely from the object itself.

	OnClientListChanged();
}

TCPServerSocket* TCPListenManagerSocket::CreateServer( void )
{
	return new TCPServerSocket;
}

void TCPListenManagerSocket::ReuseServer( TCPServerSocket* pPort )
{
}

int TCPListenManagerSocket::GetCount( void )
{
	return m_Clients.GetSize();
}

TCPServerSocket* TCPListenManagerSocket::operator[]( int idx )
{
	if( idx < m_Clients.GetSize() ) {
		return m_Clients[idx];
	}
	return NULL;
}

void TCPListenManagerSocket::OnClientListChanged( void )
{
}


///////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////
// UDPCOMMPORT
///////////////////////////////////////////////////////////////////////////////////////

UDPSocket::UDPSocket()
{
	m_uiPort				= 0;
	m_pData					= NULL;
	m_ReceivedPacketSize	= DEFAULT_DGRAM_SIZE;

	m_State					= UDP_STATE_CLOSED;
}

UDPSocket::~UDPSocket()
{
	Close();

	if( m_pData ) 
	{
		delete m_pData;
	}
}

void UDPSocket::SetReceiveBufferSize( int iSize )
{
	m_ReceivedPacketSize = iSize;

	if( m_pData ) 
	{
		delete m_pData;
	}
	m_pData = new BYTE[ m_ReceivedPacketSize ];
}


BOOL UDPSocket::OpenListenPort( UINT uiPort )
{
	SetAddress( uiPort );
	return OpenListenPort();
}

BOOL UDPSocket::OpenListenPort( void )
{
	BOOL bResult = Create( m_uiPort, SOCK_DGRAM );

	if( !bResult ) 
	{
		DisplayLastError();
		m_State	= UDP_STATE_CLOSED;
	} else {
		m_State	= UDP_STATE_OPEN;
	}

	return bResult;
}

BOOL UDPSocket::OpenPort( void )
{
	// Pass call onto create function
	BOOL bResult = Create( 0, SOCK_DGRAM );

	if( !bResult ) 
	{
		DisplayLastError();
	}
	return bResult;
}

void UDPSocket::SetAddress( UINT uiPort )
{
	m_uiPort = uiPort;
}

UINT UDPSocket::GetAddress( void )
{
	return m_uiPort;
}


void UDPSocket::Close( void )
{
	BYTE Buffer[50];
	ShutDown();
	while(Receive(Buffer,50) > 0);
	CAsyncSocket::Close();
	m_State = UDP_STATE_CLOSED;
}

BOOL UDPSocket::SendPacket( LPCTSTR pHost, UINT uiPort, PBYTE pData, int iSize )
{
	int iCount = SendTo( pData, iSize, uiPort, pHost );

	if( iCount != iSize ) 
	{
		DisplayLastError();
	}
	return iCount == iSize;
}

void UDPSocket::OnConnect( int nErrorCode )
{
	TRACE( "On Connect\n\r" );
}

void UDPSocket::OnAccept( int nErrorCode )
{
	TRACE( "On Accept\n\r" );
}

void UDPSocket::ProcessReceivedPacket( LPCTSTR pAddress, UINT uiPort, PBYTE pData, int iCount )
{
}

void UDPSocket::OnReceive( int nErrorCode )
{
	BOOL	More = TRUE;
	CString	m_szAddress;
	UINT	m_uiPort;

	while( More ) 
	{
		if( !m_pData ) 
		{
			SetReceiveBufferSize( DEFAULT_DGRAM_SIZE );
		}

		int iCount = ReceiveFrom( m_pData, m_ReceivedPacketSize, m_szAddress, m_uiPort );

		if( iCount == SOCKET_ERROR ) 
		{
			int iError = GetLastError();
			if( iError && iError != WSAEWOULDBLOCK ) 
			{
				DisplayLastError( iError );
			}
			More = FALSE;
		}
		else 
		{
			ProcessReceivedPacket( m_szAddress, m_uiPort, m_pData, iCount );
		}
	}
}

///////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////
// WINSOCK
///////////////////////////////////////////////////////////////////////////////////////

void GetErrorString ( DWORD dwError, CString& szError )
{
	LPSTR	pText = NULL;

	if ( 0 == ::FormatMessage( FORMAT_MESSAGE_ALLOCATE_BUFFER |
							   FORMAT_MESSAGE_FROM_SYSTEM | 
							   FORMAT_MESSAGE_IGNORE_INSERTS, 
							   NULL,
							   dwError, 
							   MAKELANGID(LANG_NEUTRAL, SUBLANG_DEFAULT),
							   (LPTSTR)&pText, 0, NULL ) )
	{
		pText = NULL;
	}

	szError = pText;

	if ( pText ) LocalFree( pText );
}

void CWinSocket::GetErrorMsg( CString& szString, int iErrorCode )
{
	ErrorResource * pError = m_ErrorTable;

	while( pError->Error != LAST_ERROR && pError->Error != iErrorCode )
		pError++;

	if( pError->Error == iErrorCode ) 
	{
		CString szError;
		szError.LoadString( pError->ResourceErrorCode );
		szString += szError;
	} 
	else 
	{
		szString += _T( "Unknown" );
	}
}

void CWinSocket::DisplayError( LPCTSTR pStr )
{
	::MessageBox( NULL, pStr, _T("Network Error"), MB_ICONEXCLAMATION | MB_OK );
}

void CWinSocket::DisplayError( UINT ResId )
{
	CString szError;
	VERIFY( szError.LoadString( ResId ));
	DisplayError( szError );
}

void CWinSocket::DisplayLastError( CString& szString, int iErrorCode )
{
	GetErrorMsg( szString, iErrorCode );
	DisplayError( szString );
}

void CWinSocket::DisplayLastError( CString& szString )
{
	DisplayLastError( szString, GetLastError() );
}

void CWinSocket::DisplayLastError( int iErrorCode )
{
	CString szError;
	DisplayLastError( szError, iErrorCode );
}

void CWinSocket::DisplayLastError( void )
{
	CString szError;
	DisplayLastError( szError, GetLastError() );
}

ErrorResource CWinSocket::m_ErrorTable[] = {
	{ WSAEINTR,				IDS_ERR_WSAEINTR },
	{ WSAEBADF,				IDS_ERR_WSAEBADF },
	{ WSAEACCES,			IDS_ERR_WSAEACCES },
	{ WSAEFAULT,			IDS_ERR_WSAEFAULT },
	{ WSAEINVAL,			IDS_ERR_WSAEINVAL },
	{ WSAEMFILE,			IDS_ERR_WSAEMFILE },
/*
 * Windows Sockets definitions of regular Berkeley error constants
 */
	{ WSAEWOULDBLOCK,		IDS_ERR_WSAEWOULDBLOCK },
	{ WSAEINPROGRESS,		IDS_ERR_WSAEINPROGRESS },
	{ WSAEALREADY,			IDS_ERR_WSAEALREADY },
	{ WSAENOTSOCK,			IDS_ERR_WSAENOTSOCK },
	{ WSAEDESTADDRREQ,		IDS_ERR_WSAEDESTADDRREQ },
	{ WSAEMSGSIZE,			IDS_ERR_WSAEMSGSIZE },
	{ WSAEPROTOTYPE,		IDS_ERR_WSAEPROTOTYPE },
	{ WSAENOPROTOOPT,		IDS_ERR_WSAENOPROTOOPT },
	{ WSAEPROTONOSUPPORT,	IDS_ERR_WSAEPROTONOSUPPORT },
	{ WSAESOCKTNOSUPPORT,	IDS_ERR_WSAESOCKTNOSUPPORT },
	{ WSAEOPNOTSUPP,		IDS_ERR_WSAEOPNOTSUPP },
	{ WSAEPFNOSUPPORT,		IDS_ERR_WSAEPFNOSUPPORT },
	{ WSAEAFNOSUPPORT,		IDS_ERR_WSAEAFNOSUPPORT },
	{ WSAEADDRINUSE,		IDS_ERR_WSAEADDRINUSE },
	{ WSAEADDRNOTAVAIL,		IDS_ERR_WSAEADDRNOTAVAIL },
	{ WSAENETDOWN,			IDS_ERR_WSAENETDOWN },
	{ WSAENETUNREACH,		IDS_ERR_WSAENETUNREACH },
	{ WSAENETRESET,			IDS_ERR_WSAENETRESET },
	{ WSAECONNABORTED,		IDS_ERR_WSAECONNABORTED },
	{ WSAECONNRESET,		IDS_ERR_WSAECONNRESET },
	{ WSAENOBUFS,			IDS_ERR_WSAENOBUFS },
	{ WSAEISCONN,			IDS_ERR_WSAEISCONN },
	{ WSAENOTCONN,			IDS_ERR_WSAENOTCONN },
	{ WSAESHUTDOWN,			IDS_ERR_WSAESHUTDOWN },
	{ WSAETOOMANYREFS,		IDS_ERR_WSAETOOMANYREFS },
	{ WSAETIMEDOUT,			IDS_ERR_WSAETIMEDOUT },
	{ WSAECONNREFUSED,		IDS_ERR_WSAECONNREFUSED },
	{ WSAELOOP,				IDS_ERR_WSAELOOP },
	{ WSAENAMETOOLONG,		IDS_ERR_WSAENAMETOOLONG },
	{ WSAEHOSTDOWN,			IDS_ERR_WSAEHOSTDOWN },
	{ WSAEHOSTUNREACH,		IDS_ERR_WSAEHOSTUNREACH },
	{ WSAENOTEMPTY,			IDS_ERR_WSAENOTEMPTY },
	{ WSAEPROCLIM,			IDS_ERR_WSAEPROCLIM },
	{ WSAEUSERS,			IDS_ERR_WSAEUSERS },
	{ WSAEDQUOT,			IDS_ERR_WSAEDQUOT },
	{ WSAESTALE,			IDS_ERR_WSAESTALE },
	{ WSAEREMOTE,			IDS_ERR_WSAEREMOTE },
	{ WSAEDISCON,			IDS_ERR_WSAEDISCON },
/*
 * Extended Windows Sockets error constant definitions
 */
	{ WSASYSNOTREADY,		IDS_ERR_WSASYSNOTREADY },
	{ WSAVERNOTSUPPORTED,	IDS_ERR_WSAVERNOTSUPPORTED },
	{ WSANOTINITIALISED,	IDS_ERR_WSANOTINITIALISED },
	{ LAST_ERROR,	0 },
};

IPConnectionState TCPSocket::m_StatesTable[] = {
	{ IP_STATE_CLOSED,			"Closed" },
	{ IP_STATE_CREATED,			"Created" },
	{ IP_STATE_CONNECTING,		"Connecting" },
	{ IP_STATE_CONNECTED,		"Connected" },
	{ IP_STATE_DISCONNECTING,	"Disconnecting" },
	{ IP_STATE_LISTENING,		"Listening" }
};


