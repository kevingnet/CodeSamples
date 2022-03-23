#include "NWConnect.h"

/****************************************************************************
DLL MAIN ENTRY CODE
****************************************************************************/
BOOL APIENTRY DllMain(HANDLE hInst, DWORD ul_reason_being_called, LPVOID lpReserved)
{
    return 1 ;

		UNREFERENCED_PARAMETER ( hInst ) ;
		UNREFERENCED_PARAMETER ( ul_reason_being_called ) ;
		UNREFERENCED_PARAMETER ( lpReserved ) ;
}
/****************************************************************************
****************************************************************************/

HANDLE					ghLogFile								;
DWORD					gdwBytesWritten							;
CString					gszTemp									;
CString					gszDebug								;
CString					gszErrors	=	"\tERRORS:\r\n\r\n"		;



//////////////////////////////////////////////////////////////////////////////////////////////////
// FUNCTION: Connect
// PURPOSE:
// AUTHOR: Kevin Guerra
// NOTES:
//
//////////////////////////////////////////////////////////////////////////////////////////////////
BOOL FAR APIENTRY Connect ( SERVER_INFO * si	)
{
	BOOL			bReturn						;
	if ( si->bPriorConnection == TRUE )
	{
		return ( TRUE ) ;
	}
	if ( IsConnected(si) == TRUE )
	{
		si->bPriorConnection = TRUE ;
		return ( TRUE ) ;
	}

	switch ( si->lOptions )
	{
	case 0 :
		bReturn = NWNDSConnectCurrentUser (si) ;
		if ( bReturn == FALSE )
		{
			bReturn = NWBinderyConnectUser (si) ;
			if ( bReturn == FALSE )
			{
				bReturn = NWNDSConnectUser (si) ;
			}
		}
		break ;
	case 1 :
		bReturn = NWNDSConnectCurrentUser (si) ;
		if ( bReturn == FALSE )
		{
			bReturn = NWBinderyConnectUser (si) ;
			if ( bReturn == FALSE )
			{
				bReturn = NWNDSConnectUser (si) ;
			}
		}
		break ;

	


	case 2 :
		//NWNDSConnectCurrentUser (si) ;
		NWBinderyConnectUser (si) ;
		//NWNDSConnectUser (si) ;
		//MSG("Wait")
		break ;


//	case 2 :
//		bReturn = NWNDSConnectCurrentUser (si) ;
//		if ( bReturn == FALSE )
//		{
//			bReturn = NWBinderyConnectUser (si) ;
//			if ( bReturn == FALSE )
//			{
//				bReturn = NWNDSConnectUser (si) ;
//			}
//		}
//		break ;
	case 3 :
		break ;
	case 4 :
		bReturn = NWBinderyConnectUser (si) ;
		if ( bReturn == FALSE )
		{
			bReturn = NWNDSConnectUser (si) ;
		}
		break ;
	case 5 :
		bReturn = NWNDSConnectUser (si) ;
		break ;
	}
	return ( bReturn ) ;
}
//////////////////////////////////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////////////////////////////////
// FUNCTION: Disconnect
// PURPOSE:
// AUTHOR: Kevin Guerra
// NOTES:
//
//////////////////////////////////////////////////////////////////////////////////////////////////
BOOL FAR APIENTRY Disconnect ( SERVER_INFO * si	)
{
	if ( si->bPriorConnection == TRUE )
	{
		return ( TRUE ) ;
	}

	disconnect(si) ;

	if ( IsConnected(si) == TRUE )
	{
		return ( FALSE ) ;
	}
	else
	{
		return ( TRUE ) ;
	}
}
//////////////////////////////////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////////////////////////////////
// FUNCTION: disconnect
// PURPOSE:
// AUTHOR: Kevin Guerra
// NOTES:
//
//////////////////////////////////////////////////////////////////////////////////////////////////
BOOL disconnect ( SERVER_INFO * si	)
{

//      -----------------  DECLARATIONS  ------------------------

	NWCCODE			cCode	;
	NWRCODE			rCode	;
	char	serverName	[NW_MAX_SERVER_NAME_LEN];
	char	volName		[NW_MAX_VOLUME_NAME_LEN];
	char	dirPath		[PATH_SIZE]				;
	CString					dbgBuffer							;
	bool					bHadErrors		=	false			;
	NWDSCCODE				dscCode								;
	NWCONN_HANDLE			connHandle							;
	NWCCConnInfo			connInfo							;	
	nuint32					connRef		=		0			;
	DWORD					dwResult							;

//      -----------------------  CODE  ------------------------

	gszDebug = "debug: DISCONNECT" ;

	si->szTree.MakeUpper		() ;
	si->szContext.MakeUpper	() ;
	si->szFullPath.MakeUpper	() ;
	si->szUser.MakeUpper		() ;
	si->szPassword.MakeUpper	() ;
	cCode = NWCallsInit ( NULL, NULL ) ;
	NW_EXIT_IF( cCode, NWCallsInit	 )
	cCode = NWCLXInit ( NULL, NULL	 ) ;
	NW_EXIT_IF( cCode, NWCLXInit	 )
	cCode = NWParsePath (
						si->szFullPath.GetBuffer(10),
						serverName		,
						& connHandle	,
						volName			,
						dirPath			);
	si->szFullPath.ReleaseBuffer() ;
	if ( cCode = NO_CONNECTION_TO_SERVER ) cCode = 0 ;
	NW_EXIT_IF ( cCode, NWParsePath )
	if ( connHandle )
	{	DEBUG_MSG( PREV_CONN )
		rCode = NWCCGetAllConnInfo (
									connHandle			,
									NWCC_INFO_VERSION_1	,
									& connInfo			) ;
		NW_EXIT_IF ( rCode, NWCCGetAllConnInfo			)

		if ( connInfo.serverVersion.major >= VERSION_4_X_NDS )
		{	DEBUG_MSG( NDS_CONN )
			dscCode = NWNetInit ( NULL, NULL ) ;
			NW_EXIT_IF ( dscCode, NWNetInit	 )
			cCode = NWCCGetConnRef ( connHandle, & connRef ) ;
			NW_ERROR_IF ( cCode, NWCCGetConnRef			   )
			rCode = NWCCSysCloseConnRef ( connRef ) ;
			NW_ERROR_IF ( cCode, NWCCSysCloseConnRef )
			dscCode = NWNetTerm ( NULL ) ;
			NW_ERROR_IF ( dscCode, NWNetTerm )
		}
		else
		{	DEBUG_MSG( BINDERY_CONN )
			cCode = NWLogoutFromFileServer ( connHandle ) ;
			NW_EXIT_IF ( cCode, NWLogoutFromFileServer	)
			cCode = NWFreeConnectionSlot ( connHandle, SYSTEM_DISCONNECT ) ;
			NW_ERROR_IF ( cCode, NWFreeConnectionSlot )
		}
	}
	else
	{
		DEBUG_MSG( NOT_PREV_CONN )
	}
	cCode = NWCLXTerm ( NULL ) ;
	NW_ERROR_IF( cCode, NWCLXTerm	)
	cCode = NWCallsTerm ( NULL ) ;

	if ( bHadErrors )
	{
		DEBUG_MSG( SUCCESS_WITH_ERRORS_SZ )
		return ( FALSE ) ;
	}
	else
	{
		DEBUG_MSG( SUCCESS_NO_ERRORS_SZ )
		return ( TRUE	  ) ;
	}
}
//////////////////////////////////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////////////////////////////////
// FUNCTION: IsConnected
// PURPOSE:
// AUTHOR: Kevin Guerra
// NOTES:
//
//////////////////////////////////////////////////////////////////////////////////////////////////
BOOL FAR APIENTRY IsConnected ( SERVER_INFO * si	)
{

	if ( si->bPriorConnection == TRUE )
	{
		return ( TRUE ) ;
	}

//      -----------------  DECLARATIONS  ------------------------

	NWCCODE			cCode	;
	NWRCODE			rCode	;
	char	serverName	[NW_MAX_SERVER_NAME_LEN];
	char	volName		[NW_MAX_VOLUME_NAME_LEN];
	char	dirPath		[PATH_SIZE]				;
	CString					dbgBuffer							;
	bool					bHadErrors		=	false			;
	NWCONN_HANDLE			connHandle							;
	NWCCConnInfo			connInfo							;	
	int						bIsConnected	= FALSE				;

//      -----------------------  CODE  ------------------------

	gszDebug = "debug: ISCONNECTED" ;

	si->szTree.MakeUpper		() ;
	si->szContext.MakeUpper	() ;
	si->szFullPath.MakeUpper	() ;
	si->szUser.MakeUpper		() ;
	si->szPassword.MakeUpper	() ;
	cCode = NWCallsInit ( NULL, NULL ) ;
	NW_EXIT_IF( cCode, NWCallsInit	 )
	cCode = NWCLXInit ( NULL, NULL	 ) ;
	NW_EXIT_IF( cCode, NWCLXInit	 )
	cCode = NWParsePath (
						si->szFullPath.GetBuffer(10),
						serverName		,
						& connHandle	,
						volName			,
						dirPath			);
	si->szFullPath.ReleaseBuffer() ;
	if ( cCode = NO_CONNECTION_TO_SERVER ) cCode = 0 ;
	NW_EXIT_IF ( cCode, NWParsePath )
	if ( connHandle )
	{	DEBUG_MSG( PREV_CONN )
		rCode = NWCCGetAllConnInfo (
									connHandle			,
									NWCC_INFO_VERSION_1	,
									& connInfo			) ;
		NW_EXIT_IF ( rCode, NWCCGetAllConnInfo			)

		switch ( connInfo.authenticationState )
		{
			case NWCC_AUTHENT_STATE_NDS:
				DEBUG_MSG( NDS_CONN )
				if ( connInfo.licenseState == NWCC_NOT_LICENSED )
				{	DEBUG_MSG( UNLICESED_CONNECTION )
					bIsConnected = FALSE ;
				}
				else
				{	DEBUG_MSG( LICESED_CONNECTION )
					bIsConnected = TRUE ;
				}
				break;
			
			case NWCC_AUTHENT_STATE_BIND:
				DEBUG_MSG( BINDERY_CONN )
				if ( connInfo.licenseState == NWCC_NOT_LICENSED )
				{	DEBUG_MSG( UNLICESED_CONNECTION )
					bIsConnected = FALSE ;
				}
				else
				{	DEBUG_MSG( LICESED_CONNECTION )
					bIsConnected = TRUE ;
				}
				break;

			case NWCC_AUTHENT_STATE_NONE:
			default :
				DEBUG_MSG( INVALID_CONN )
				bIsConnected = FALSE ;
				break;
		}
	}
	else
	{	DEBUG_MSG( NOT_PREV_CONN )
		bIsConnected = FALSE ;
	}

	cCode = NWCLXTerm ( NULL ) ;
	NW_ERROR_IF( cCode, NWCLXTerm	)
	cCode = NWCallsTerm ( NULL ) ;
	return ( bIsConnected ) ;
}
//////////////////////////////////////////////////////////////////////////////////////////////////

//////////////////////////////////////////////////////////////////////////////////////////////////
// FUNCTION: NWNDSConnectCurrentUser
// PURPOSE:
// AUTHOR: Kevin Guerra
// NOTES:
//
//////////////////////////////////////////////////////////////////////////////////////////////////
BOOL NWNDSConnectCurrentUser ( SERVER_INFO * si	)
{

//      -----------------  DECLARATIONS  ------------------------

	NWCCODE			cCode	;
	NWRCODE			rCode	;
	char	serverName	[NW_MAX_SERVER_NAME_LEN];
	char	volName		[NW_MAX_VOLUME_NAME_LEN];
	char	dirPath		[PATH_SIZE]				;
	CString					dbgBuffer							;
	bool					bHadErrors		=	false			;
	NWDSCCODE				dscCode								;
	NWCONN_HANDLE			connHandle							;
	NWCONN_HANDLE			serverHandle						;
	NWDSContextHandle		dContext							;
	NWCCConnInfo			connInfo							;	
	nuint32					flags								;
	nuint32					connRef		=	0				;
	char					userName	[MAX_USER]	= {"0"}		; 

//      -----------------------  CODE  ------------------------

	gszDebug = "debug: NWNDSConnectCurrentUser" ;

	si->szTree.MakeUpper		() ;
	si->szContext.MakeUpper	() ;
	si->szFullPath.MakeUpper	() ;
	si->szUser.MakeUpper		() ;
	si->szPassword.MakeUpper	() ;
	cCode = NWCallsInit ( NULL, NULL ) ;
	NW_EXIT_IF( cCode, NWCallsInit	 )
	cCode = NWCLXInit ( NULL, NULL	 ) ;
	NW_EXIT_IF( cCode, NWCLXInit	 )
	cCode = NWParsePath (
						si->szFullPath.GetBuffer(10),
						serverName		,
						& connHandle	,
						volName			,
						dirPath			);
	si->szFullPath.ReleaseBuffer() ;
	if ( cCode = NO_CONNECTION_TO_SERVER ) cCode = 0 ;
	NW_EXIT_IF ( cCode, NWParsePath )

	dscCode = NWDSCreateContextHandle ( & dContext ) ;
	NW_EXIT_IF ( dscCode, NWDSCreateContextHandle  ) 
	dscCode = NWDSSetContext (
								dContext		,
								DCK_TREE_NAME	,
								si->szTree.GetBuffer(10) ) ;
	si->szTree.ReleaseBuffer() ;
	NW_EXIT_IF ( dscCode, NWDSSetContext )
	if ( strlen( si->szContext ) == 0 )
	{	/*SET ROOT CONTEXT*/
		dscCode = NWDSSetContext (
									dContext			,
									DCK_NAME_CONTEXT	,
									DS_ROOT_NAME		) ;
		NW_EXIT_IF ( dscCode, NWDSSetContext			)
	}
	else
	{	/*SET PASSED CONTEXT*/
		dscCode = NWDSSetContext (
									dContext			,
									DCK_NAME_CONTEXT	,
									si->szContext.GetBuffer(10) ) ;
		si->szContext.ReleaseBuffer() ;
		NW_EXIT_IF ( dscCode, NWDSSetContext			)
		/*SET DEFAULT NAME CONTEXT*/
		dscCode = NWDSSetDefNameContext (
										dContext					,
										strlen ( si->szContext )	,
										si->szContext.GetBuffer(10)) ;
		si->szContext.ReleaseBuffer() ;
		NW_EXIT_IF ( dscCode, NWDSSetDefNameContext					)
	}
	dscCode = NWDSGetContext (
								dContext	,
								DCK_FLAGS	,
								& flags		) ;
	NW_EXIT_IF ( dscCode, NWDSGetContext	)
	/* ALTER FLAGS - we want typeless names */
	flags |= DCV_TYPELESS_NAMES	;
	dscCode = NWDSSetContext (
								dContext	,
								DCK_FLAGS	,
								& flags		) ;
	NW_EXIT_IF ( dscCode, NWDSSetContext	)
	rCode = NWCCOpenConnByName (
								0					,
								serverName				,
								NWCC_NAME_FORMAT_BIND	,
								NWCC_OPEN_LICENSED		,
	/*WILD/IPX*/				NWCC_TRAN_TYPE_IPX		,
								& serverHandle			) ;
	NW_EXIT_IF ( rCode, NWCCOpenConnByName				)
	dscCode = NWDSAuthenticate ( serverHandle, 0, NULL ) ;
	NW_EXIT_IF ( dscCode, NWDSAuthenticate				  )
	rCode = NWCCLicenseConn ( serverHandle ) ;
	NW_EXIT_IF ( rCode, NWCCLicenseConn	   )
	rCode = NWCCMakeConnPermanent ( serverHandle ) ;
	NW_EXIT_IF ( rCode, NWCCMakeConnPermanent	     )
	dscCode = NWDSFreeContext ( dContext ) ;
	NW_ERROR_IF ( dscCode, NWDSFreeContext  )
	if ( si->iBroadcastState != connInfo.broadcastState )
	{
		cCode = NWSetBroadcastMode ( serverHandle, si->iBroadcastState ) ;
		NW_ERROR_IF ( cCode, NWSetBroadcastMode						 )
	}
	dscCode = NWNetTerm ( NULL ) ;
	NW_ERROR_IF ( dscCode, NWNetTerm )

	cCode = NWCLXTerm ( NULL ) ;
	NW_ERROR_IF( cCode, NWCLXTerm	)
	cCode = NWCallsTerm ( NULL ) ;

	if ( bHadErrors )
	{
		DEBUG_MSG( SUCCESS_WITH_ERRORS_SZ )
		return ( FALSE ) ;
	}
	else
	{
		DEBUG_MSG( SUCCESS_NO_ERRORS_SZ )
		return ( TRUE	  ) ;
	}
}

//////////////////////////////////////////////////////////////////////////////////////////////////
// FUNCTION: NWBinderyConnectUser
// PURPOSE:
// AUTHOR: Kevin Guerra
// NOTES:
//
//////////////////////////////////////////////////////////////////////////////////////////////////
BOOL NWBinderyConnectUser ( SERVER_INFO * si )
{

//      -----------------  DECLARATIONS  ------------------------

	NWCCODE			cCode	;
	NWRCODE			rCode	;
	char	serverName	[NW_MAX_SERVER_NAME_LEN];
	char	volName		[NW_MAX_VOLUME_NAME_LEN];
	char	dirPath		[PATH_SIZE]				;
	CString					dbgBuffer							;
	bool					bHadErrors		=	false			;
	NWCONN_HANDLE			connHandle							;
	NWCCConnInfo			connInfo							;	
	nuint16					objType								;
	nuint32					connRef			=	0			;
	char					userName	[MAX_USER]				;

//      -----------------------  CODE  ------------------------

	gszDebug = "debug: NWBinderyConnectUser" ;

	si->szTree.MakeUpper		() ;
	si->szContext.MakeUpper	() ;
	si->szFullPath.MakeUpper	() ;
	si->szUser.MakeUpper		() ;
	si->szPassword.MakeUpper	() ;
	cCode = NWCallsInit ( NULL, NULL ) ;
	NW_EXIT_IF( cCode, NWCallsInit	 )
	cCode = NWCLXInit ( NULL, NULL	 ) ;
	NW_EXIT_IF( cCode, NWCLXInit	 )
	cCode = NWParsePath (
						si->szFullPath.GetBuffer(10),
						serverName		,
						& connHandle	,
						volName			,
						dirPath			);
	si->szFullPath.ReleaseBuffer() ;
	if ( cCode = NO_CONNECTION_TO_SERVER ) cCode = 0 ;
	NW_EXIT_IF ( cCode, NWParsePath )

	DEBUG_MSG( BINDERY_ATTEMPT )
	if ( connHandle )
	{	DEBUG_MSG( PREV_CONN )
		rCode = NWCCGetAllConnInfo (
									connHandle			,
									NWCC_INFO_VERSION_1	,
									& connInfo			) ;
		NW_EXIT_IF ( rCode, NWCCGetAllConnInfo			)
		switch ( connInfo.authenticationState )
		{
			case NWCC_AUTHENT_STATE_NDS:
				DEBUG_MSG( NDS_CONN )
				cCode = NWLogoutFromFileServer ( connHandle ) ;
				NW_EXIT_IF ( cCode, NWLogoutFromFileServer	)
				cCode = NWLoginToFileServer (
											connHandle		,
											si->szUser.GetBuffer(10),
											OT_USER			,
											si->szPassword.GetBuffer(10)	) ;
				si->szUser.ReleaseBuffer() ;
				si->szPassword.ReleaseBuffer() ;
				NW_EXIT_IF ( cCode, NWLoginToFileServer	)
				rCode = NWCCLicenseConn ( connHandle ) ;
				NW_EXIT_IF ( rCode, NWCCLicenseConn	 )
				rCode = NWCCMakeConnPermanent ( connHandle ) ;
				NW_EXIT_IF ( rCode, NWCCMakeConnPermanent  )
				break;
			
			case NWCC_AUTHENT_STATE_BIND:
				DEBUG_MSG( BINDERY_CONN )
				cCode = NWGetObjectName (
										connHandle			,
										connInfo.userID		,
										(char*) & userName	,
										& objType			) ;
				if ( ! cCode )
				{	DEBUG_MSG( GOT_USER_NAME )
					if ( strcmp ( strupr ( userName ), si->szUser ) == 0 )
					{	DEBUG_MSG( USER_CREDENTIALS_MATCHED )
						if ( connInfo.licenseState == NWCC_NOT_LICENSED )
						{	DEBUG_MSG( UNLICESED_CONNECTION )
							rCode = NWCCLicenseConn ( connHandle ) ;
							NW_EXIT_IF ( rCode, NWCCLicenseConn	 )
						}
						rCode = NWCCMakeConnPermanent ( connHandle ) ;
						NW_EXIT_IF ( rCode, NWCCMakeConnPermanent  )
					}
					else
					{	DEBUG_MSG( USER_CREDENTIALS_DID_NOT_MATCH )
						cCode = NWLogoutFromFileServer ( connHandle ) ;
						NW_EXIT_IF ( cCode, NWLogoutFromFileServer	)
						cCode = NWLoginToFileServer (
													connHandle		,
													si->szUser.GetBuffer(10),
													OT_USER			,
													si->szPassword.GetBuffer(10)	) ;
						si->szUser.ReleaseBuffer() ;
						si->szPassword.ReleaseBuffer() ;
						NW_EXIT_IF ( cCode, NWLoginToFileServer	)
						rCode = NWCCLicenseConn ( connHandle ) ;
						NW_EXIT_IF ( rCode, NWCCLicenseConn	 )
						rCode = NWCCMakeConnPermanent ( connHandle ) ;
						NW_EXIT_IF ( rCode, NWCCMakeConnPermanent  )
					}
				}
				else
				{	DEBUG_MSG( DID_NOT_GET_USER_NAME )
					cCode = NWLogoutFromFileServer ( connHandle ) ;
					NW_EXIT_IF ( cCode, NWLogoutFromFileServer	)
					cCode = NWLoginToFileServer (
												connHandle		,
												si->szUser.GetBuffer(10),
												OT_USER			,
												si->szPassword.GetBuffer(10)	) ;
					si->szUser.ReleaseBuffer() ;
					si->szPassword.ReleaseBuffer() ;
					NW_EXIT_IF ( cCode, NWLoginToFileServer	)
					rCode = NWCCLicenseConn ( connHandle ) ;
					NW_EXIT_IF ( rCode, NWCCLicenseConn	 )
					rCode = NWCCMakeConnPermanent ( connHandle ) ;
					NW_EXIT_IF ( rCode, NWCCMakeConnPermanent  )
				}
				break;

			case NWCC_AUTHENT_STATE_NONE:
			default :
				DEBUG_MSG( INVALID_CONN )
				cCode = NWLoginToFileServer (
											connHandle		,
											si->szUser.GetBuffer(10),
											OT_USER			,
											si->szPassword.GetBuffer(10)	) ;
				si->szUser.ReleaseBuffer() ;
				si->szPassword.ReleaseBuffer() ;
				NW_EXIT_IF ( cCode, NWLoginToFileServer	)
				rCode = NWCCLicenseConn ( connHandle ) ;
				NW_EXIT_IF ( rCode, NWCCLicenseConn	 )
				rCode = NWCCMakeConnPermanent ( connHandle ) ;
				NW_EXIT_IF ( rCode, NWCCMakeConnPermanent  )
				break;
		}
	}
	else
	{	DEBUG_MSG( NOT_PREV_CONN )
		rCode = NWCCOpenConnByName (
									0					,
									(pnstr8)serverName		,
									NWCC_NAME_FORMAT_BIND	,
									NWCC_OPEN_LICENSED		,
									NWCC_RESERVED			,
									& connHandle			) ;
		NW_EXIT_IF ( rCode, NWCCOpenConnByName				)
		cCode = NWLoginToFileServer (
									connHandle		,
									si->szUser.GetBuffer(10),
									OT_USER			,
									si->szPassword.GetBuffer(10)	) ;
		si->szUser.ReleaseBuffer() ;
		si->szPassword.ReleaseBuffer() ;
		NW_EXIT_IF ( cCode, NWLoginToFileServer	)
		rCode = NWCCLicenseConn ( connHandle ) ;
		NW_EXIT_IF ( rCode, NWCCLicenseConn	 )
		rCode = NWCCMakeConnPermanent ( connHandle ) ;
		NW_EXIT_IF ( rCode, NWCCMakeConnPermanent  )
	}
	if ( si->iBroadcastState != connInfo.broadcastState )
	{
		cCode = NWSetBroadcastMode ( connHandle, si->iBroadcastState ) ;
		NW_ERROR_IF ( cCode, NWSetBroadcastMode						 )
	}

	cCode = NWCLXTerm ( NULL ) ;
	NW_ERROR_IF( cCode, NWCLXTerm	)
	cCode = NWCallsTerm ( NULL ) ;
	if ( bHadErrors )
	{
		DEBUG_MSG( SUCCESS_WITH_ERRORS_SZ )
		return ( FALSE ) ;
	}
	else
	{
		DEBUG_MSG( SUCCESS_NO_ERRORS_SZ )
		return ( TRUE	  ) ;
	}
}

//////////////////////////////////////////////////////////////////////////////////////////////////
// FUNCTION: NWNDSConnectUser
// PURPOSE:
// AUTHOR: Kevin Guerra
// NOTES:
//
//////////////////////////////////////////////////////////////////////////////////////////////////
BOOL NWNDSConnectUser ( SERVER_INFO * si )
{

//      -----------------  DECLARATIONS  ------------------------

	NWCCODE			cCode	;
	NWRCODE			rCode	;
	char	serverName	[NW_MAX_SERVER_NAME_LEN];
	char	volName		[NW_MAX_VOLUME_NAME_LEN];
	char	dirPath		[PATH_SIZE]				;
	CString					dbgBuffer							;
	bool					bHadErrors		=	false			;
	nuint32				iterator			=	0			;
	char				serverList[20][NW_MAX_SERVER_NAME_LEN]	;
	unsigned int		i										;
	unsigned int		j										;
	NWDSCCODE				dscCode								;
	NWCONN_HANDLE			connHandle							;
	NWCONN_HANDLE			serverHandle						;
	NWDSContextHandle		dContext							;
	NWCCConnInfo			connInfo							;	
	nuint32					flags								;
	nuint32					connRef			=	0			;
	char					userName	[MAX_USER]				;
	char			szPrompt	[STRING_BUFFER]	;
	char			szTitle		[STRING_BUFFER]	;

//      -----------------------  CODE  ------------------------

	switch ( si->lNDSCredentialsChange )
	{
	case 0:
		return ( FALSE ) ;
		break ;
	case 1:
		break ;
	default :
		if ( MessageBox ( NULL,
						  szPrompt,
						  szTitle,
						  MB_YESNO | MB_ICONEXCLAMATION ) == IDNO ) 
		{
			return ( FALSE ) ;
		}
		break ;
	}

	gszDebug = "debug: NWNDSConnectUser" ;

	si->szTree.MakeUpper		() ;
	si->szContext.MakeUpper	() ;
	si->szFullPath.MakeUpper	() ;
	si->szUser.MakeUpper		() ;
	si->szPassword.MakeUpper	() ;
	cCode = NWCallsInit ( NULL, NULL ) ;
	NW_EXIT_IF( cCode, NWCallsInit	 )
	cCode = NWCLXInit ( NULL, NULL	 ) ;
	NW_EXIT_IF( cCode, NWCLXInit	 )
	cCode = NWParsePath (
						si->szFullPath.GetBuffer(10),
						serverName		,
						& connHandle	,
						volName			,
						dirPath			);
	si->szFullPath.ReleaseBuffer() ;
	if ( cCode = NO_CONNECTION_TO_SERVER ) cCode = 0 ;
	NW_EXIT_IF ( cCode, NWParsePath )

	DEBUG_MSG( NDS_ATTEMPT )
	NW_EXIT_IF ( ( si->szTree.GetLength() == 0 )	, NO_TREE_NAME )
	dscCode = NWNetInit ( NULL, NULL ) ;
	NW_EXIT_IF ( dscCode, NWNetInit	 )
	if ( connHandle )
	{	DEBUG_MSG( PREV_CONN )

		rCode = NWCCGetAllConnInfo (
									connHandle			,
									NWCC_INFO_VERSION_1	,
									& connInfo			) ;
		NW_EXIT_IF ( rCode, NWCCGetAllConnInfo			)
		NW_EXIT_IF ( ( connInfo.NDSState  == NWCC_NDS_NOT_CAPABLE	), NOT_AN_NDS_SERVER )
		switch ( connInfo.authenticationState )
		{
			case NWCC_AUTHENT_STATE_NDS:
				DEBUG_MSG( NDS_CONN )
				dscCode = NWDSCreateContextHandle ( & dContext ) ;
				NW_EXIT_IF ( dscCode, NWDSCreateContextHandle  ) 
				dscCode = NWDSSetContext (
											dContext		,
											DCK_TREE_NAME	,
											si->szTree.GetBuffer(10) ) ;
				si->szTree.ReleaseBuffer() ;
				NW_EXIT_IF ( dscCode, NWDSSetContext )
				if ( strlen( si->szContext ) == 0 )
				{	/*SET ROOT CONTEXT*/
					dscCode = NWDSSetContext (
												dContext			,
												DCK_NAME_CONTEXT	,
												DS_ROOT_NAME		) ;
					NW_EXIT_IF ( dscCode, NWDSSetContext			)
				}
				else
				{	/*SET PASSED CONTEXT*/
					dscCode = NWDSSetContext (
												dContext			,
												DCK_NAME_CONTEXT	,
												si->szContext.GetBuffer(10) ) ;
					si->szContext.ReleaseBuffer() ;
					NW_EXIT_IF ( dscCode, NWDSSetContext			)
					/*SET DEFAULT NAME CONTEXT*/
					dscCode = NWDSSetDefNameContext (
													dContext					,
													strlen ( si->szContext )	,
													si->szContext.GetBuffer(10)) ;
					si->szContext.ReleaseBuffer() ;
					NW_EXIT_IF ( dscCode, NWDSSetDefNameContext					)
				}
				dscCode = NWDSGetContext (
											dContext	,
											DCK_FLAGS	,
											& flags		) ;
				NW_EXIT_IF ( dscCode, NWDSGetContext	)
				/* ALTER FLAGS - we want typeless names */
				flags |= DCV_TYPELESS_NAMES	;
				dscCode = NWDSSetContext (
											dContext	,
											DCK_FLAGS	,
											& flags		) ;
	NW_EXIT_IF ( dscCode, NWDSSetContext	)
				dscCode = NWDSWhoAmI ( dContext, userName ) ;
				NW_ERROR_IF ( dscCode, NWDSWhoAmI		  )
				dscCode = NWDSFreeContext ( dContext ) ;
				NW_ERROR_IF ( dscCode, NWDSFreeContext  )
				if ( ! cCode )
				{	DEBUG_MSG( GOT_USER_NAME )
					if ( strcmp ( strupr ( userName ), si->szUser ) == 0 )
					{	DEBUG_MSG( USER_CREDENTIALS_MATCHED )
					}
					else
					{	DEBUG_MSG( USER_CREDENTIALS_DID_NOT_MATCH )
	j=0;
	while ( NWCCScanConnRefs( & iterator, & connRef ) == 0 )
	{
		rCode = NWCCGetAllConnRefInfo (
									connRef					,
									NWCC_INFO_VERSION_1		,
									& connInfo				) ;
		NW_EXIT_IF ( rCode, NWCCGetAllConnRefInfo			)
		for ( i=0; i<=strlen(connInfo.treeName); i++ )
		{
			if ( connInfo.treeName[i] == '_' )
			{
				connInfo.treeName[i] = 0 ;
			}
		}
		if (	strcmp ( strupr ( connInfo.treeName ), si->szTree ) == 0 &&
				connInfo.authenticationState == NWCC_AUTHENT_STATE_NDS &&
				connInfo.licenseState == NWCC_NOT_LICENSED )
		{
			strcpy ( serverList[j], connInfo.serverName ) ;
			rCode = NWCCOpenConnByRef (
										connRef					,
										NWCC_OPEN_UNLICENSED	,
										NWCC_RESERVED			,
										& connHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByRef					)
			rCode = NWCCUnlicenseConn ( connHandle ) ;
			j++ ;
		}
		if (	strcmp ( strupr ( connInfo.treeName ), si->szTree ) == 0 &&
				strcmp ( strupr ( connInfo.serverName ), serverName ) == 0 &&
				connInfo.authenticationState == NWCC_AUTHENT_STATE_BIND )
		{
			strcpy ( serverList[j], connInfo.serverName ) ;
			rCode = NWCCOpenConnByRef (
										connRef					,
										NWCC_OPEN_UNLICENSED	,
										NWCC_RESERVED			,
										& connHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByRef				)
			cCode = NWLogoutFromFileServer ( connHandle ) ;
			NW_EXIT_IF ( cCode, NWLogoutFromFileServer	)
			j++ ;
		}
	}
	j--;
	dscCode = NWDSCreateContextHandle ( & dContext ) ;
	NW_EXIT_IF ( dscCode, NWDSCreateContextHandle  ) 
	dscCode = NWDSLogout ( dContext					) ;
	NW_ERROR_IF ( dscCode, NWDSCreateContextHandle	)
	dscCode = NWDSLogin (
						dContext		,
						0			,
						si->szUser.GetBuffer(10),
						si->szPassword.GetBuffer(10),
						0			) ;
	si->szUser.ReleaseBuffer() ;
	si->szPassword.ReleaseBuffer() ;
	NW_EXIT_IF ( dscCode, NWDSLogin		)
	for ( i=0; i<=j; i++ )
	{
		strcpy ( serverName, serverList[i] ) ;
		if ( strlen ( serverName ) > 0 )
		{
			rCode = NWCCOpenConnByName (
										0					,
										serverName				,
										NWCC_NAME_FORMAT_BIND	,
										NWCC_OPEN_LICENSED		,
			/*WILD/IPX*/				NWCC_TRAN_TYPE_IPX		,
										& serverHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByName				)
			dscCode = NWDSAuthenticate ( serverHandle, 0, NULL ) ;
			NW_EXIT_IF ( dscCode, NWDSAuthenticate				  )
			rCode = NWCCLicenseConn ( serverHandle ) ;
			NW_EXIT_IF ( rCode, NWCCLicenseConn	   )
			rCode = NWCCMakeConnPermanent ( serverHandle ) ;
			NW_EXIT_IF ( rCode, NWCCMakeConnPermanent	     )
		}
	}
	dscCode = NWDSFreeContext ( dContext ) ;
	NW_ERROR_IF ( dscCode, NWDSFreeContext  )
					}
				}
				else
				{	DEBUG_MSG( DID_NOT_GET_USER_NAME )
	j=0;
	while ( NWCCScanConnRefs( & iterator, & connRef ) == 0 )
	{
		rCode = NWCCGetAllConnRefInfo (
									connRef					,
									NWCC_INFO_VERSION_1		,
									& connInfo				) ;
		NW_EXIT_IF ( rCode, NWCCGetAllConnRefInfo			)
		for ( i=0; i<=strlen(connInfo.treeName); i++ )
		{
			if ( connInfo.treeName[i] == '_' )
			{
				connInfo.treeName[i] = 0 ;
			}
		}
		if (	strcmp ( strupr ( connInfo.treeName ), si->szTree ) == 0 &&
				connInfo.authenticationState == NWCC_AUTHENT_STATE_NDS &&
				connInfo.licenseState == NWCC_NOT_LICENSED )
		{
			strcpy ( serverList[j], connInfo.serverName ) ;
			rCode = NWCCOpenConnByRef (
										connRef					,
										NWCC_OPEN_UNLICENSED	,
										NWCC_RESERVED			,
										& connHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByRef					)
			rCode = NWCCUnlicenseConn ( connHandle ) ;
			j++ ;
		}
		if (	strcmp ( strupr ( connInfo.treeName ), si->szTree ) == 0 &&
				strcmp ( strupr ( connInfo.serverName ), serverName ) == 0 &&
				connInfo.authenticationState == NWCC_AUTHENT_STATE_BIND )
		{
			strcpy ( serverList[j], connInfo.serverName ) ;
			rCode = NWCCOpenConnByRef (
										connRef					,
										NWCC_OPEN_UNLICENSED	,
										NWCC_RESERVED			,
										& connHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByRef				)
			cCode = NWLogoutFromFileServer ( connHandle ) ;
			NW_EXIT_IF ( cCode, NWLogoutFromFileServer	)
			j++ ;
		}
	}
	j--;
	dscCode = NWDSCreateContextHandle ( & dContext ) ;
	NW_EXIT_IF ( dscCode, NWDSCreateContextHandle  ) 
	dscCode = NWDSLogout ( dContext					) ;
	NW_ERROR_IF ( dscCode, NWDSCreateContextHandle	)
	dscCode = NWDSLogin (
						dContext		,
						0			,
						si->szUser.GetBuffer(10),
						si->szPassword.GetBuffer(10),
						0			) ;
	si->szUser.ReleaseBuffer() ;
	si->szPassword.ReleaseBuffer() ;
	NW_EXIT_IF ( dscCode, NWDSLogin		)
	for ( i=0; i<=j; i++ )
	{
		strcpy ( serverName, serverList[i] ) ;
		if ( strlen ( serverName ) > 0 )
		{
			rCode = NWCCOpenConnByName (
										0					,
										serverName				,
										NWCC_NAME_FORMAT_BIND	,
										NWCC_OPEN_LICENSED		,
			/*WILD/IPX*/				NWCC_TRAN_TYPE_IPX		,
										& serverHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByName				)
			dscCode = NWDSAuthenticate ( serverHandle, 0, NULL ) ;
			NW_EXIT_IF ( dscCode, NWDSAuthenticate				  )
			rCode = NWCCLicenseConn ( serverHandle ) ;
			NW_EXIT_IF ( rCode, NWCCLicenseConn	   )
			rCode = NWCCMakeConnPermanent ( serverHandle ) ;
			NW_EXIT_IF ( rCode, NWCCMakeConnPermanent	     )
		}
	}
	dscCode = NWDSFreeContext ( dContext ) ;
	NW_ERROR_IF ( dscCode, NWDSFreeContext  )
				}
				break;

			case NWCC_AUTHENT_STATE_BIND:
				DEBUG_MSG( BINDERY_CONN )
				dscCode = NWDSCreateContextHandle ( & dContext ) ;
				NW_EXIT_IF ( dscCode, NWDSCreateContextHandle  ) 
				dscCode = NWDSSetContext (
											dContext		,
											DCK_TREE_NAME	,
											si->szTree.GetBuffer(10) ) ;
				si->szTree.ReleaseBuffer() ;
				NW_EXIT_IF ( dscCode, NWDSSetContext )
				if ( strlen( si->szContext ) == 0 )
				{	/*SET ROOT CONTEXT*/
					dscCode = NWDSSetContext (
												dContext			,
												DCK_NAME_CONTEXT	,
												DS_ROOT_NAME		) ;
					NW_EXIT_IF ( dscCode, NWDSSetContext			)
				}
				else
				{	/*SET PASSED CONTEXT*/
					dscCode = NWDSSetContext (
												dContext			,
												DCK_NAME_CONTEXT	,
												si->szContext.GetBuffer(10) ) ;
					si->szContext.ReleaseBuffer() ;
					NW_EXIT_IF ( dscCode, NWDSSetContext			)
					/*SET DEFAULT NAME CONTEXT*/
					dscCode = NWDSSetDefNameContext (
													dContext					,
													strlen ( si->szContext )	,
													si->szContext.GetBuffer(10)) ;
					si->szContext.ReleaseBuffer() ;
					NW_EXIT_IF ( dscCode, NWDSSetDefNameContext					)
				}
				dscCode = NWDSGetContext (
											dContext	,
											DCK_FLAGS	,
											& flags		) ;
				NW_EXIT_IF ( dscCode, NWDSGetContext	)
				/* ALTER FLAGS - we want typeless names */
				flags |= DCV_TYPELESS_NAMES	;
				dscCode = NWDSSetContext (
											dContext	,
											DCK_FLAGS	,
											& flags		) ;
				dscCode = NWDSWhoAmI ( dContext, userName ) ;
				NW_ERROR_IF ( dscCode, NWDSWhoAmI		  )
				dscCode = NWDSFreeContext ( dContext ) ;
				NW_ERROR_IF ( dscCode, NWDSFreeContext  )
				if ( ! cCode )
				{	DEBUG_MSG( GOT_USER_NAME )
					if ( strcmp ( strupr ( userName ), si->szUser ) == 0 )
					{	DEBUG_MSG( USER_CREDENTIALS_MATCHED )
						cCode = NWLogoutFromFileServer ( connHandle ) ;
						NW_EXIT_IF ( cCode, NWLogoutFromFileServer	)
						rCode = NWCCOpenConnByName (
													0					,
													serverName				,
													NWCC_NAME_FORMAT_BIND	,
													NWCC_OPEN_LICENSED		,
						/*WILD/IPX*/				NWCC_TRAN_TYPE_IPX		,
													& serverHandle			) ;
						NW_EXIT_IF ( rCode, NWCCOpenConnByName				)
						dscCode = NWDSAuthenticate ( serverHandle, 0, NULL ) ;
						NW_EXIT_IF ( dscCode, NWDSAuthenticate				  )
						rCode = NWCCLicenseConn ( serverHandle ) ;
						NW_EXIT_IF ( rCode, NWCCLicenseConn	   )
						rCode = NWCCMakeConnPermanent ( serverHandle ) ;
						NW_EXIT_IF ( rCode, NWCCMakeConnPermanent	     )
					}
					else
					{	DEBUG_MSG( USER_CREDENTIALS_DID_NOT_MATCH )
	j=0;
	while ( NWCCScanConnRefs( & iterator, & connRef ) == 0 )
	{
		rCode = NWCCGetAllConnRefInfo (
									connRef					,
									NWCC_INFO_VERSION_1		,
									& connInfo				) ;
		NW_EXIT_IF ( rCode, NWCCGetAllConnRefInfo			)
		for ( i=0; i<=strlen(connInfo.treeName); i++ )
		{
			if ( connInfo.treeName[i] == '_' )
			{
				connInfo.treeName[i] = 0 ;
			}
		}
		if (	strcmp ( strupr ( connInfo.treeName ), si->szTree ) == 0 &&
				connInfo.authenticationState == NWCC_AUTHENT_STATE_NDS &&
				connInfo.licenseState == NWCC_NOT_LICENSED )
		{
			strcpy ( serverList[j], connInfo.serverName ) ;
			rCode = NWCCOpenConnByRef (
										connRef					,
										NWCC_OPEN_UNLICENSED	,
										NWCC_RESERVED			,
										& connHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByRef					)
			rCode = NWCCUnlicenseConn ( connHandle ) ;
			j++ ;
		}
		if (	strcmp ( strupr ( connInfo.treeName ), si->szTree ) == 0 &&
				strcmp ( strupr ( connInfo.serverName ), serverName ) == 0 &&
				connInfo.authenticationState == NWCC_AUTHENT_STATE_BIND )
		{
			strcpy ( serverList[j], connInfo.serverName ) ;
			rCode = NWCCOpenConnByRef (
										connRef					,
										NWCC_OPEN_UNLICENSED	,
										NWCC_RESERVED			,
										& connHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByRef				)
			cCode = NWLogoutFromFileServer ( connHandle ) ;
			NW_EXIT_IF ( cCode, NWLogoutFromFileServer	)
			j++ ;
		}
	}
	j--;
	dscCode = NWDSCreateContextHandle ( & dContext ) ;
	NW_EXIT_IF ( dscCode, NWDSCreateContextHandle  ) 
	dscCode = NWDSLogout ( dContext					) ;
	NW_ERROR_IF ( dscCode, NWDSCreateContextHandle	)
	dscCode = NWDSLogin (
						dContext		,
						0			,
						si->szUser.GetBuffer(10),
						si->szPassword.GetBuffer(10),
						0			) ;
	si->szUser.ReleaseBuffer() ;
	si->szPassword.ReleaseBuffer() ;
	NW_EXIT_IF ( dscCode, NWDSLogin		)
	for ( i=0; i<=j; i++ )
	{
		strcpy ( serverName, serverList[i] ) ;
		if ( strlen ( serverName ) > 0 )
		{
			rCode = NWCCOpenConnByName (
										0					,
										serverName				,
										NWCC_NAME_FORMAT_BIND	,
										NWCC_OPEN_LICENSED		,
			/*WILD/IPX*/				NWCC_TRAN_TYPE_IPX		,
										& serverHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByName				)
			dscCode = NWDSAuthenticate ( serverHandle, 0, NULL ) ;
			NW_EXIT_IF ( dscCode, NWDSAuthenticate				  )
			rCode = NWCCLicenseConn ( serverHandle ) ;
			NW_EXIT_IF ( rCode, NWCCLicenseConn	   )
			rCode = NWCCMakeConnPermanent ( serverHandle ) ;
			NW_EXIT_IF ( rCode, NWCCMakeConnPermanent	     )
		}
	}
	dscCode = NWDSFreeContext ( dContext ) ;
	NW_ERROR_IF ( dscCode, NWDSFreeContext  )
					}
				}
				else
				{	DEBUG_MSG( DID_NOT_GET_USER_NAME )
	j=0;
	while ( NWCCScanConnRefs( & iterator, & connRef ) == 0 )
	{
		rCode = NWCCGetAllConnRefInfo (
									connRef					,
									NWCC_INFO_VERSION_1		,
									& connInfo				) ;
		NW_EXIT_IF ( rCode, NWCCGetAllConnRefInfo			)
		for ( i=0; i<=strlen(connInfo.treeName); i++ )
		{
			if ( connInfo.treeName[i] == '_' )
			{
				connInfo.treeName[i] = 0 ;
			}
		}
		if (	strcmp ( strupr ( connInfo.treeName ), si->szTree ) == 0 &&
				connInfo.authenticationState == NWCC_AUTHENT_STATE_NDS &&
				connInfo.licenseState == NWCC_NOT_LICENSED )
		{
			strcpy ( serverList[j], connInfo.serverName ) ;
			rCode = NWCCOpenConnByRef (
										connRef					,
										NWCC_OPEN_UNLICENSED	,
										NWCC_RESERVED			,
										& connHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByRef					)
			rCode = NWCCUnlicenseConn ( connHandle ) ;
			j++ ;
		}
		if (	strcmp ( strupr ( connInfo.treeName ), si->szTree ) == 0 &&
				strcmp ( strupr ( connInfo.serverName ), serverName ) == 0 &&
				connInfo.authenticationState == NWCC_AUTHENT_STATE_BIND )
		{
			strcpy ( serverList[j], connInfo.serverName ) ;
			rCode = NWCCOpenConnByRef (
										connRef					,
										NWCC_OPEN_UNLICENSED	,
										NWCC_RESERVED			,
										& connHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByRef				)
			cCode = NWLogoutFromFileServer ( connHandle ) ;
			NW_EXIT_IF ( cCode, NWLogoutFromFileServer	)
			j++ ;
		}
	}
	j--;
	dscCode = NWDSCreateContextHandle ( & dContext ) ;
	NW_EXIT_IF ( dscCode, NWDSCreateContextHandle  ) 
	dscCode = NWDSLogout ( dContext					) ;
	NW_ERROR_IF ( dscCode, NWDSCreateContextHandle	)
	dscCode = NWDSLogin (
						dContext		,
						0			,
						si->szUser.GetBuffer(10),
						si->szPassword.GetBuffer(10),
						0			) ;
	si->szUser.ReleaseBuffer() ;
	si->szPassword.ReleaseBuffer() ;
	NW_EXIT_IF ( dscCode, NWDSLogin		)
	for ( i=0; i<=j; i++ )
	{
		strcpy ( serverName, serverList[i] ) ;
		if ( strlen ( serverName ) > 0 )
		{
			rCode = NWCCOpenConnByName (
										0					,
										serverName				,
										NWCC_NAME_FORMAT_BIND	,
										NWCC_OPEN_LICENSED		,
			/*WILD/IPX*/				NWCC_TRAN_TYPE_IPX		,
										& serverHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByName				)
			dscCode = NWDSAuthenticate ( serverHandle, 0, NULL ) ;
			NW_EXIT_IF ( dscCode, NWDSAuthenticate				  )
			rCode = NWCCLicenseConn ( serverHandle ) ;
			NW_EXIT_IF ( rCode, NWCCLicenseConn	   )
			rCode = NWCCMakeConnPermanent ( serverHandle ) ;
			NW_EXIT_IF ( rCode, NWCCMakeConnPermanent	     )
		}
	}
	dscCode = NWDSFreeContext ( dContext ) ;
	NW_ERROR_IF ( dscCode, NWDSFreeContext  )
				}
				break;

			case NWCC_AUTHENT_STATE_NONE:
			default :
				DEBUG_MSG( INVALID_CONN )
	j=0;
	while ( NWCCScanConnRefs( & iterator, & connRef ) == 0 )
	{
		rCode = NWCCGetAllConnRefInfo (
									connRef					,
									NWCC_INFO_VERSION_1		,
									& connInfo				) ;
		NW_EXIT_IF ( rCode, NWCCGetAllConnRefInfo			)
		for ( i=0; i<=strlen(connInfo.treeName); i++ )
		{
			if ( connInfo.treeName[i] == '_' )
			{
				connInfo.treeName[i] = 0 ;
			}
		}
		if (	strcmp ( strupr ( connInfo.treeName ), si->szTree ) == 0 &&
				connInfo.authenticationState == NWCC_AUTHENT_STATE_NDS &&
				connInfo.licenseState == NWCC_NOT_LICENSED )
		{
			strcpy ( serverList[j], connInfo.serverName ) ;
			rCode = NWCCOpenConnByRef (
										connRef					,
										NWCC_OPEN_UNLICENSED	,
										NWCC_RESERVED			,
										& connHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByRef					)
			rCode = NWCCUnlicenseConn ( connHandle ) ;
			j++ ;
		}
		if (	strcmp ( strupr ( connInfo.treeName ), si->szTree ) == 0 &&
				strcmp ( strupr ( connInfo.serverName ), serverName ) == 0 &&
				connInfo.authenticationState == NWCC_AUTHENT_STATE_BIND )
		{
			strcpy ( serverList[j], connInfo.serverName ) ;
			rCode = NWCCOpenConnByRef (
										connRef					,
										NWCC_OPEN_UNLICENSED	,
										NWCC_RESERVED			,
										& connHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByRef				)
			cCode = NWLogoutFromFileServer ( connHandle ) ;
			NW_EXIT_IF ( cCode, NWLogoutFromFileServer	)
			j++ ;
		}
	}
	j--;
	dscCode = NWDSCreateContextHandle ( & dContext ) ;
	NW_EXIT_IF ( dscCode, NWDSCreateContextHandle  ) 
	dscCode = NWDSLogout ( dContext					) ;
	NW_ERROR_IF ( dscCode, NWDSCreateContextHandle	)
	dscCode = NWDSLogin (
						dContext		,
						0			,
						si->szUser.GetBuffer(10),
						si->szPassword.GetBuffer(10),
						0			) ;
	si->szUser.ReleaseBuffer() ;
	si->szPassword.ReleaseBuffer() ;
	NW_EXIT_IF ( dscCode, NWDSLogin		)
	for ( i=0; i<=j; i++ )
	{
		strcpy ( serverName, serverList[i] ) ;
		if ( strlen ( serverName ) > 0 )
		{
			rCode = NWCCOpenConnByName (
										0					,
										serverName				,
										NWCC_NAME_FORMAT_BIND	,
										NWCC_OPEN_LICENSED		,
			/*WILD/IPX*/				NWCC_TRAN_TYPE_IPX		,
										& serverHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByName				)
			dscCode = NWDSAuthenticate ( serverHandle, 0, NULL ) ;
			NW_EXIT_IF ( dscCode, NWDSAuthenticate				  )
			rCode = NWCCLicenseConn ( serverHandle ) ;
			NW_EXIT_IF ( rCode, NWCCLicenseConn	   )
			rCode = NWCCMakeConnPermanent ( serverHandle ) ;
			NW_EXIT_IF ( rCode, NWCCMakeConnPermanent	     )
		}
	}
	dscCode = NWDSFreeContext ( dContext ) ;
	NW_ERROR_IF ( dscCode, NWDSFreeContext  )
				break;
		}
	}
	else
	{	DEBUG_MSG( NOT_PREV_CONN )
	j=0;
	while ( NWCCScanConnRefs( & iterator, & connRef ) == 0 )
	{
		rCode = NWCCGetAllConnRefInfo (
									connRef					,
									NWCC_INFO_VERSION_1		,
									& connInfo				) ;
		NW_EXIT_IF ( rCode, NWCCGetAllConnRefInfo			)
		for ( i=0; i<=strlen(connInfo.treeName); i++ )
		{
			if ( connInfo.treeName[i] == '_' )
			{
				connInfo.treeName[i] = 0 ;
			}
		}
		if (	strcmp ( strupr ( connInfo.treeName ), si->szTree ) == 0 &&
				connInfo.authenticationState == NWCC_AUTHENT_STATE_NDS &&
				connInfo.licenseState == NWCC_NOT_LICENSED )
		{
			strcpy ( serverList[j], connInfo.serverName ) ;
			rCode = NWCCOpenConnByRef (
										connRef					,
										NWCC_OPEN_UNLICENSED	,
										NWCC_RESERVED			,
										& connHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByRef					)
			rCode = NWCCUnlicenseConn ( connHandle ) ;
			j++ ;
		}
		if (	strcmp ( strupr ( connInfo.treeName ), si->szTree ) == 0 &&
				strcmp ( strupr ( connInfo.serverName ), serverName ) == 0 &&
				connInfo.authenticationState == NWCC_AUTHENT_STATE_BIND )
		{
			strcpy ( serverList[j], connInfo.serverName ) ;
			rCode = NWCCOpenConnByRef (
										connRef					,
										NWCC_OPEN_UNLICENSED	,
										NWCC_RESERVED			,
										& connHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByRef				)
			cCode = NWLogoutFromFileServer ( connHandle ) ;
			NW_EXIT_IF ( cCode, NWLogoutFromFileServer	)
			j++ ;
		}
	}
	j--;
	dscCode = NWDSCreateContextHandle ( & dContext ) ;
	NW_EXIT_IF ( dscCode, NWDSCreateContextHandle  ) 
	dscCode = NWDSLogout ( dContext					) ;
	NW_ERROR_IF ( dscCode, NWDSCreateContextHandle	)
	dscCode = NWDSLogin (
						dContext		,
						0			,
						si->szUser.GetBuffer(10),
						si->szPassword.GetBuffer(10),
						0			) ;
	si->szUser.ReleaseBuffer() ;
	si->szPassword.ReleaseBuffer() ;
	NW_EXIT_IF ( dscCode, NWDSLogin		)
	for ( i=0; i<=j; i++ )
	{
		strcpy ( serverName, serverList[i] ) ;
		if ( strlen ( serverName ) > 0 )
		{
			rCode = NWCCOpenConnByName (
										0					,
										serverName				,
										NWCC_NAME_FORMAT_BIND	,
										NWCC_OPEN_LICENSED		,
			/*WILD/IPX*/				NWCC_TRAN_TYPE_IPX		,
										& serverHandle			) ;
			NW_EXIT_IF ( rCode, NWCCOpenConnByName				)
			dscCode = NWDSAuthenticate ( serverHandle, 0, NULL ) ;
			NW_EXIT_IF ( dscCode, NWDSAuthenticate				  )
			rCode = NWCCLicenseConn ( serverHandle ) ;
			NW_EXIT_IF ( rCode, NWCCLicenseConn	   )
			rCode = NWCCMakeConnPermanent ( serverHandle ) ;
			NW_EXIT_IF ( rCode, NWCCMakeConnPermanent	     )
		}
	}
	dscCode = NWDSFreeContext ( dContext ) ;
	NW_ERROR_IF ( dscCode, NWDSFreeContext  )
	}
	if ( si->iBroadcastState != connInfo.broadcastState )
	{
		cCode = NWSetBroadcastMode ( serverHandle, si->iBroadcastState ) ;
		NW_ERROR_IF ( cCode, NWSetBroadcastMode						 ) 
	}
	dscCode = NWNetTerm ( NULL ) ;
	NW_ERROR_IF ( dscCode, NWNetTerm )
	cCode = NWCLXTerm ( NULL ) ;
	NW_ERROR_IF( cCode, NWCLXTerm	)
	cCode = NWCallsTerm ( NULL ) ;
	if ( bHadErrors )
	{
		DEBUG_MSG( SUCCESS_WITH_ERRORS_SZ )
		return ( FALSE ) ;
	}
	else
	{
		DEBUG_MSG( SUCCESS_NO_ERRORS_SZ )
		return ( TRUE	  ) ;
	}
}





CString &  GetMSError( LONG lError, CString & szString )
{

switch ( lError )
{

case EPT_S_CANT_CREATE :
	szString = "The endpoint mapper database could not be created." ;
	break;
	
case EPT_S_CANT_PERFORM_OP :
	szString = "The server endpoint cannot perform the operation." ;
	break;
	
case EPT_S_INVALID_ENTRY :
	szString = "The entry is invalid." ;
	break;
	
case EPT_S_NOT_REGISTERED :
	szString = "There are no more endpoints available from the endpoint mapper." ;
	break;
	
case ERROR_ACCESS_DENIED :
	szString = "Access is denied." ;
	break;
	
case ERROR_ACCOUNT_DISABLED :
	szString = "Logon failure: account currently disabled." ;
	break;
	
case ERROR_ACCOUNT_EXPIRED :
	szString = "The user's account has expired." ;
	break;
	
case ERROR_ACCOUNT_LOCKED_OUT :
	szString = "The referenced account is currently locked out and may not be logged on to." ;
	break;
	
case ERROR_ACCOUNT_RESTRICTION :
	szString = "Logon failure: user account restriction." ;
	break;
	
case ERROR_ACTIVE_CONNECTIONS :
	szString = "Active connections still exist." ;
	break;
	
case ERROR_ADAP_HDW_ERR :
	szString = "A network adapter hardware error occurred." ;
	break;
	
case ERROR_ADDRESS_ALREADY_ASSOCIATED :
	szString = "The network transport endpoint already has an address associated with it." ;
	break;
	
case ERROR_ADDRESS_NOT_ASSOCIATED :
	szString = "An address has not yet been associated with the network endpoint." ;
	break;
	
case ERROR_ALIAS_EXISTS :
	szString = "The specified local group already exists." ;
	break;
	
case ERROR_ALLOTTED_SPACE_EXCEEDED :
	szString = "No more memory is available for security information updates." ;
	break;
	
case ERROR_ALREADY_ASSIGNED :
	szString = "The local device name is already in use." ;
	break;
	
case ERROR_ALREADY_EXISTS :
	szString = "Cannot create a file when that file already exists." ;
	break;
	
case ERROR_ALREADY_INITIALIZED :
	szString = "An attempt was made to perform an initialization operation when initialization has already been completed." ;
	break;
	
case ERROR_ALREADY_REGISTERED :
	szString = "The service is already registered." ;
	break;
	
case ERROR_ALREADY_RUNNING_LKG :
	szString = "The system is currently running with the last-known-good configuration." ;
	break;
	
case ERROR_ALREADY_WAITING :
	szString = "The specified printer handle is already being waited on" ;
	break;
	
case ERROR_APP_WRONG_OS :
	szString = "The specified program is not a Windows or MS-DOS program." ;
	break;
	
case ERROR_ARENA_TRASHED :
	szString = "The storage control blocks were destroyed." ;
	break;
	
case ERROR_ARITHMETIC_OVERFLOW :
	szString = "Arithmetic result exceeded 32 bits." ;
	break;
	
case ERROR_ATOMIC_LOCKS_NOT_SUPPORTED :
	szString = "The file system does not support atomic changes to the lock type." ;
	break;
	
case ERROR_AUTODATASEG_EXCEEDS_64k :
	szString = "The operating system cannot run this application program." ;
	break;
	
case ERROR_BAD_ARGUMENTS :
	szString = "The argument string passed to DosExecPgm is not correct." ;
	break;
	
case ERROR_BAD_COMMAND :
	szString = "The device does not recognize the command." ;
	break;
	
case ERROR_BAD_DESCRIPTOR_FORMAT :
	szString = "A security descriptor is not in the right format (absolute or self-relative)." ;
	break;
	
case ERROR_BAD_DEV_TYPE :
	szString = "The network resource type is not correct." ;
	break;
	
case ERROR_BAD_DEVICE :
	szString = "The specified device name is invalid." ;
	break;
	
case ERROR_BAD_DRIVER :
	szString = "The specified driver is invalid." ;
	break;
	
case ERROR_BAD_DRIVER_LEVEL :
	szString = "The system does not support the command requested." ;
	break;
	
case ERROR_BAD_ENVIRONMENT :
	szString = "The environment is incorrect." ;
	break;
	
case ERROR_BAD_EXE_FORMAT :
	szString = "Is not a valid application." ;
	break;
	
case ERROR_BAD_FORMAT :
	szString = "An attempt was made to load a program with an incorrect format." ;
	break;
	
case ERROR_BAD_IMPERSONATION_LEVEL :
	szString = "Either a required impersonation level was not provided, or the provided impersonation level is invalid." ;
	break;
	
case ERROR_BAD_INHERITANCE_ACL :
	szString = "The inherited access control list (ACL) or access control entry (ACE) could not be built." ;
	break;
	
case ERROR_BAD_LENGTH :
	szString = "The program issued a command but the command length is incorrect." ;
	break;
	
case ERROR_BAD_LOGON_SESSION_STATE :
	szString = "The logon session is not in a state that is consistent with the requested operation." ;
	break;
	
case ERROR_BAD_NET_NAME :
	szString = "The network name cannot be found." ;
	break;
	
case ERROR_BAD_NET_RESP :
	szString = "The specified server cannot perform the requested operation." ;
	break;
	
case ERROR_BAD_NETPATH :
	szString = "The network path was not found." ;
	break;
	
case ERROR_BAD_PATHNAME :
	szString = "The specified path is invalid." ;
	break;
	
case ERROR_BAD_PIPE :
	szString = "The pipe state is invalid." ;
	break;
	
case ERROR_BAD_PROFILE :
	szString = "The network connection profile is corrupted." ;
	break;
	
case ERROR_BAD_PROVIDER :
	szString = "The specified network provider name is invalid." ;
	break;
	
case ERROR_BAD_REM_ADAP :
	szString = "The remote adapter is not compatible." ;
	break;
	
case ERROR_BAD_THREADID_ADDR :
	szString = "The address for the thread ID is not correct." ;
	break;
	
case ERROR_BAD_TOKEN_TYPE :
	szString = "The type of the token is inappropriate for its attempted use." ;
	break;
	
case ERROR_BAD_UNIT :
	szString = "The system cannot find the device specified." ;
	break;
	
case ERROR_BAD_USERNAME :
	szString = "The specified user name is invalid." ;
	break;
	
case ERROR_BAD_VALIDATION_CLASS :
	szString = "The validation information class requested was invalid." ;
	break;
	
case ERROR_BADDB :
	szString = "The configuration registry database is corrupt." ;
	break;
	
case ERROR_BADKEY :
	szString = "The configuration registry key is invalid." ;
	break;
	
case ERROR_BEGINNING_OF_MEDIA :
	szString = "The beginning of the tape or partition was encountered." ;
	break;
	
case ERROR_BOOT_ALREADY_ACCEPTED :
	szString = "The current boot has already been accepted for use as the last-known-good control set." ;
	break;
	
case ERROR_BROKEN_PIPE :
	szString = "The pipe has been ended." ;
	break;
	
case ERROR_BUFFER_OVERFLOW :
	szString = "The file name is too long." ;
	break;
	
case ERROR_BUS_RESET :
	szString = "The I/O bus was reset." ;
	break;
	
case ERROR_BUSY :
	szString = "The requested resource is in use." ;
	break;
	
case ERROR_BUSY_DRIVE :
	szString = "The system cannot perform a JOIN or SUBST at this time." ;
	break;
	
case ERROR_CALL_NOT_IMPLEMENTED :
	szString = "This function is not valid on this platform." ;
	break;
	
case ERROR_CAN_NOT_COMPLETE :
	szString = "Cannot complete this function." ;
	break;
	
case ERROR_CAN_NOT_DEL_LOCAL_WINS :
	szString = "The local WINS can not be deleted." ;
	break;
	
case ERROR_CANCEL_VIOLATION :
	szString = "A lock request was not outstanding for the supplied cancel region." ;
	break;
	
case ERROR_CANCELLED :
	szString = "The operation was canceled by the user." ;
	break;
	
case ERROR_CANNOT_COPY :
	szString = "The copy functions cannot be used." ;
	break;
	
case ERROR_CANNOT_FIND_WND_CLASS :
	szString = "Cannot find window class." ;
	break;
	
case ERROR_CANNOT_IMPERSONATE :
	szString = "Unable to impersonate using a named pipe until data has been read from that pipe." ;
	break;
	
case ERROR_CANNOT_MAKE :
	szString = "The directory or file cannot be created." ;
	break;
	
case ERROR_CANNOT_OPEN_PROFILE :
	szString = "Unable to open the network connection profile." ;
	break;
	
case ERROR_CANT_ACCESS_DOMAIN_INFO :
	szString = "Indicates a Windows NT Server could not be contacted or that objects within the domain are protected such that necessary information could not be retrieved." ;
	break;
	
case ERROR_CANT_DISABLE_MANDATORY :
	szString = "The group may not be disabled." ;
	break;
	
case ERROR_CANT_OPEN_ANONYMOUS :
	szString = "Cannot open an anonymous level security token." ;
	break;
	
case ERROR_CANTOPEN :
	szString = "The configuration registry key could not be opened." ;
	break;
	
case ERROR_CANTREAD :
	szString = "The configuration registry key could not be read." ;
	break;
	
case ERROR_CANTWRITE :
	szString = "The configuration registry key could not be written." ;
	break;
	
case ERROR_CHILD_MUST_BE_VOLATILE :
	szString = "Cannot create a stable subkey under a volatile parent key." ;
	break;
	
case ERROR_CHILD_NOT_COMPLETE :
	szString = "The %1 application cannot be run in Windows NT mode." ;
	break;
	
case ERROR_CHILD_WINDOW_MENU :
	szString = "Child windows cannot have menus." ;
	break;
	
case ERROR_CIRCULAR_DEPENDENCY :
	szString = "Circular service dependency was specified." ;
	break;
	
case ERROR_CLASS_ALREADY_EXISTS :
	szString = "Class already exists." ;
	break;
	
case ERROR_CLASS_DOES_NOT_EXIST :
	szString = "Class does not exist." ;
	break;
	
case ERROR_CLASS_HAS_WINDOWS :
	szString = "Class still has open windows." ;
	break;
	
case ERROR_CLIPBOARD_NOT_OPEN :
	szString = "Thread does not have a clipboard open." ;
	break;
	
case ERROR_CLIPPING_NOT_SUPPORTED :
	szString = "The requested clipping operation is not supported." ;
	break;
	
case ERROR_COMMITMENT_LIMIT :
	szString = "The paging file is too small for this operation to complete." ;
	break;
	
case ERROR_CONNECTION_ABORTED :
	szString = "The network connection was aborted by the local system." ;
	break;
	
case ERROR_CONNECTION_ACTIVE :
	szString = "An invalid operation was attempted on an active network connection." ;
	break;
	
case ERROR_CONNECTION_COUNT_LIMIT :
	szString = "A connection to the server could not be made because the limit on the number of concurrent connections for this account has been reached." ;
	break;
	
case ERROR_CONNECTION_INVALID :
	szString = "An operation was attempted on a nonexistent network connection." ;
	break;
	
case ERROR_CONNECTION_REFUSED :
	szString = "The remote system refused the network connection." ;
	break;
	
case ERROR_CONNECTION_UNAVAIL :
	szString = "The device is not currently connected but it is a remembered connection." ;
	break;
	
case ERROR_CONTINUE :
	szString = "Continue with work in progress." ;
	break;
	
case ERROR_CONTROL_ID_NOT_FOUND :
	szString = "Control ID not found." ;
	break;
	
case ERROR_COUNTER_TIMEOUT :
	szString = "A serial I/O operation completed because the time-out period expired. (The IOCTL_SERIAL_XOFF_COUNTER did not reach zero.)" ;
	break;
	
case ERROR_CRC :
	szString = "Data error (cyclic redundancy check)." ;
	break;
	
case ERROR_CURRENT_DIRECTORY :
	szString = "The directory cannot be removed." ;
	break;
	
case ERROR_DATABASE_DOES_NOT_EXIST :
	szString = "The database specified does not exist." ;
	break;
	
case ERROR_DC_NOT_FOUND :
	szString = "Invalid device context (DC) handle." ;
	break;
	
case ERROR_DDE_FAIL :
	szString = "An error occurred in sending the command to the application." ;
	break;
	
case ERROR_DEPENDENT_SERVICES_RUNNING :
	szString = "A stop control has been sent to a service that other running services are dependent on." ;
	break;
	
case ERROR_DESTROY_OBJECT_OF_OTHER_THREAD :
	szString = "Cannot destroy object created by another thread." ;
	break;
	
case ERROR_DEV_NOT_EXIST :
	szString = "The specified network resource or device is no longer available." ;
	break;
	
case ERROR_DEVICE_ALREADY_REMEMBERED :
	szString = "An attempt was made to remember a device that had previously been remembered." ;
	break;
	
case ERROR_DEVICE_IN_USE :
	szString = "The device is in use by an active process and cannot be disconnected." ;
	break;
	
case ERROR_DEVICE_NOT_PARTITIONED :
	szString = "Tape partition information could not be found when loading a tape." ;
	break;
	
case ERROR_DIFFERENT_SERVICE_ACCOUNT :
	szString = "The account specified for this service is different from the account specified for other services running in the same process." ;
	break;
	
case ERROR_DIR_NOT_EMPTY :
	szString = "The directory is not empty." ;
	break;
	
case ERROR_DIR_NOT_ROOT :
	szString = "The directory is not a subdirectory of the root directory." ;
	break;
	
case ERROR_DIRECT_ACCESS_HANDLE :
	szString = "Attempt to use a file handle to an open disk partition for an operation other than raw disk I/O." ;
	break;
	
case ERROR_DIRECTORY :
	szString = "The directory name is invalid." ;
	break;
	
case ERROR_DISCARDED :
	szString = "The segment is already discarded and cannot be locked." ;
	break;
	
case ERROR_DISK_CHANGE :
	szString = "The program stopped because an alternate diskette was not inserted." ;
	break;
	
case ERROR_DISK_CORRUPT :
	szString = "The disk structure is corrupted and non-readable." ;
	break;
	
case ERROR_DISK_FULL :
	szString = "There is not enough space on the disk." ;
	break;
	
case ERROR_DISK_OPERATION_FAILED :
	szString = "While accessing the hard disk, a disk operation failed even after retries." ;
	break;
	
case ERROR_DISK_RECALIBRATE_FAILED :
	szString = "While accessing the hard disk, a recalibrate operation failed, even after retries." ;
	break;
	
case ERROR_DISK_RESET_FAILED :
	szString = "While accessing the hard disk, a disk controller reset was needed, but even that failed." ;
	break;
	
case ERROR_DLL_INIT_FAILED :
	szString = "A dynamic link library (DLL) initialization routine failed." ;
	break;
	
case ERROR_DLL_NOT_FOUND :
	szString = "One of the library files needed to run this application cannot be found." ;
	break;
	
case ERROR_DOMAIN_CONTROLLER_NOT_FOUND :
	szString = "Could not find the domain controller for this domain." ;
	break;
	
case ERROR_DOMAIN_EXISTS :
	szString = "The specified domain already exists." ;
	break;
	
case ERROR_DOMAIN_LIMIT_EXCEEDED :
	szString = "An attempt was made to exceed the limit on the number of domains per server." ;
	break;
	
case ERROR_DOMAIN_TRUST_INCONSISTENT :
	szString = "The name or security ID (SID) of the domain specified is inconsistent with the trust information for that domain." ;
	break;
	
case ERROR_DRIVE_LOCKED :
	szString = "The disk is in use or locked by another process." ;
	break;
	
case ERROR_DUP_DOMAINNAME :
	szString = "The workgroup or domain name is already in use by another computer on the network." ;
	break;
	
case ERROR_DUP_NAME :
	szString = "A duplicate name exists on the network." ;
	break;
	
case ERROR_DUPLICATE_SERVICE_NAME :
	szString = "The name is already in use as either a service name or a service display name." ;
	break;
	
case ERROR_DYNLINK_FROM_INVALID_RING :
	szString = "The operating system cannot run this application program." ;
	break;
	
case ERROR_EA_ACCESS_DENIED :
	szString = "Access to the extended attribute was denied." ;
	break;
	
case ERROR_EA_FILE_CORRUPT :
	szString = "The extended attribute file on the mounted file system is corrupt." ;
	break;
	
case ERROR_EA_LIST_INCONSISTENT :
	szString = "The extended attributes are inconsistent." ;
	break;
	
case ERROR_EA_TABLE_FULL :
	szString = "The extended attribute table file is full." ;
	break;
	
case ERROR_EAS_DIDNT_FIT :
	szString = "The extended attributes did not fit in the buffer." ;
	break;
	
case ERROR_EAS_NOT_SUPPORTED :
	szString = "The mounted file system does not support extended attributes." ;
	break;
	
case ERROR_END_OF_MEDIA :
	szString = "The physical end of the tape has been reached." ;
	break;
	
case ERROR_ENVVAR_NOT_FOUND :
	szString = "The system could not find the environment option that was entered." ;
	break;
	
case ERROR_EOM_OVERFLOW :
	szString = "Physical end of tape encountered." ;
	break;
	
case ERROR_EVENTLOG_CANT_START :
	szString = "No event log file could be opened, so the event logging service did not start." ;
	break;
	
case ERROR_EVENTLOG_FILE_CHANGED :
	szString = "The event log file has changed between read operations." ;
	break;
	
case ERROR_EVENTLOG_FILE_CORRUPT :
	szString = "The event log file is corrupted." ;
	break;
	
case ERROR_EXCEPTION_IN_SERVICE :
	szString = "An exception occurred in the service when handling the control request." ;
	break;
	
case ERROR_EXCL_SEM_ALREADY_OWNED :
	szString = "The exclusive semaphore is owned by another process." ;
	break;
	
case ERROR_EXE_MACHINE_TYPE_MISMATCH :
	szString = "The image file %1 is valid, but is for a machine type other than the current machine." ;
	break;
	
case ERROR_EXE_MARKED_INVALID :
	szString = "The operating system cannot run %1." ;
	break;
	
case ERROR_EXTENDED_ERROR :
	szString = "An extended error has occurred." ;
	break;
	
case ERROR_FAIL_I24 :
	szString = "Fail on INT 24." ;
	break;
	
case ERROR_FAILED_SERVICE_CONTROLLER_CONNECT :
	szString = "The service process could not connect to the service controller." ;
	break;
	
case ERROR_FILE_CORRUPT :
	szString = "The file or directory is corrupted and non-readable." ;
	break;
	
case ERROR_FILE_EXISTS :
	szString = "The file exists." ;
	break;
	
case ERROR_FILE_INVALID :
	szString = "The volume for a file has been externally altered so that the opened file is no longer valid." ;
	break;
	
case ERROR_FILE_NOT_FOUND :
	szString = "The system cannot find the file specified." ;
	break;
	
case ERROR_FILEMARK_DETECTED :
	szString = "A tape access reached a filemark." ;
	break;
	
case ERROR_FILENAME_EXCED_RANGE :
	szString = "The filename or extension is too long." ;
	break;
	
case ERROR_FLOPPY_BAD_REGISTERS :
	szString = "The floppy disk controller returned inconsistent results in its registers." ;
	break;
	
case ERROR_FLOPPY_ID_MARK_NOT_FOUND :
	szString = "No ID address mark was found on the floppy disk." ;
	break;
	
case ERROR_FLOPPY_UNKNOWN_ERROR :
	szString = "The floppy disk controller reported an error that is not recognized by the floppy disk driver." ;
	break;
	
case ERROR_FLOPPY_WRONG_CYLINDER :
	szString = "Mismatch between the floppy disk sector ID field and the floppy disk controller track address." ;
	break;
	
case ERROR_FULL_BACKUP :
	szString = "The backup failed. Check the directory to which you are backing the database." ;
	break;
	
case ERROR_FULLSCREEN_MODE :
	szString = "The requested operation cannot be performed in full-screen mode." ;
	break;
	
case ERROR_GEN_FAILURE :
	szString = "A device attached to the system is not functioning." ;
	break;
	
case ERROR_GENERIC_NOT_MAPPED :
	szString = "Generic access types were contained in an access mask which should already be mapped to nongeneric types." ;
	break;
	
case ERROR_GLOBAL_ONLY_HOOK :
	szString = "This hook procedure can only be set globally." ;
	break;
	
case ERROR_GRACEFUL_DISCONNECT :
	szString = "The network connection was gracefully closed." ;
	break;
	
case ERROR_GROUP_EXISTS :
	szString = "The specified group already exists." ;
	break;
	
case ERROR_HANDLE_DISK_FULL :
	szString = "The disk is full." ;
	break;
	
case ERROR_HANDLE_EOF :
	szString = "Reached the end of the file." ;
	break;
	
case ERROR_HOOK_NEEDS_HMOD :
	szString = "Cannot set nonlocal hook without a module handle." ;
	break;
	
case ERROR_HOOK_NOT_INSTALLED :
	szString = "The hook procedure is not installed." ;
	break;
	
case ERROR_HOOK_TYPE_NOT_ALLOWED :
	szString = "Hook type not allowed." ;
	break;
	
case ERROR_HOST_UNREACHABLE :
	szString = "The remote system is not reachable by the transport." ;
	break;
	
case ERROR_HOTKEY_ALREADY_REGISTERED :
	szString = "Hot key is already registered." ;
	break;
	
case ERROR_HOTKEY_NOT_REGISTERED :
	szString = "Hot key is not registered." ;
	break;
	
case ERROR_HWNDS_HAVE_DIFF_PARENT :
	szString = "All handles to windows in a multiple-window position structure must have the same parent." ;
	break;
	
case ERROR_ILL_FORMED_PASSWORD :
	szString = "Unable to update the password. The value provided for the new password contains values that are not allowed in passwords." ;
	break;
	
case ERROR_INC_BACKUP :
	szString = "The backup failed. Was a full backup done before?" ;
	break;
	
case ERROR_INCORRECT_ADDRESS :
	szString = "The network address could not be used for the operation requested." ;
	break;
	
case ERROR_INFLOOP_IN_RELOC_CHAIN :
	szString = "The operating system cannot run %1." ;
	break;
	
case ERROR_INSUFFICIENT_BUFFER :
	szString = "The data area passed to a system call is too small." ;
	break;
	
case ERROR_INTERNAL_DB_CORRUPTION :
	szString = "Unable to complete the requested operation because of either a catastrophic media failure or a data structure corruption on the disk." ;
	break;
	
case ERROR_INTERNAL_DB_ERROR :
	szString = "The local security authority database contains an internal inconsistency." ;
	break;
	
case ERROR_INTERNAL_ERROR :
	szString = "The security account database contains an internal inconsistency." ;
	break;
	
case ERROR_INVALID_ACCEL_HANDLE :
	szString = "Invalid accelerator table handle." ;
	break;
	
case ERROR_INVALID_ACCESS :
	szString = "The access code is invalid." ;
	break;
	
case ERROR_INVALID_ACCOUNT_NAME :
	szString = "The name provided is not a properly formed account name." ;
	break;
	
case ERROR_INVALID_ACL :
	szString = "The access control list (ACL) structure is invalid." ;
	break;
	
case ERROR_INVALID_ADDRESS :
	szString = "Attempt to access invalid address." ;
	break;
	
case ERROR_INVALID_AT_INTERRUPT_TIME :
	szString = "Cannot request exclusive semaphores at interrupt time." ;
	break;
	
case ERROR_INVALID_BLOCK :
	szString = "The storage control block address is invalid." ;
	break;
	
case ERROR_INVALID_BLOCK_LENGTH :
	szString = "When accessing a new tape of a multivolume partition, the current block size is incorrect." ;
	break;
	
case ERROR_INVALID_CATEGORY :
	szString = "The IOCTL call made by the application program is not correct." ;
	break;
	
case ERROR_INVALID_COMBOBOX_MESSAGE :
	szString = "Invalid message for a combo box because it does not have an edit control." ;
	break;
	
case ERROR_INVALID_COMPUTERNAME :
	szString = "The format of the specified computer name is invalid." ;
	break;
	
case ERROR_INVALID_CURSOR_HANDLE :
	szString = "Invalid cursor handle." ;
	break;
	
case ERROR_INVALID_DATA :
	szString = "The data is invalid." ;
	break;
	
case ERROR_INVALID_DATATYPE :
	szString = "The specified data type is invalid." ;
	break;
	
case ERROR_INVALID_DLL :
	szString = "One of the library files needed to run this application is damaged." ;
	break;
	
case ERROR_INVALID_DOMAIN_ROLE :
	szString = "This operation is only allowed for the Primary Domain Controller of the domain." ;
	break;
	
case ERROR_INVALID_DOMAIN_STATE :
	szString = "The domain was in the wrong state to perform the security operation." ;
	break;
	
case ERROR_INVALID_DOMAINNAME :
	szString = "The format of the specified domain name is invalid." ;
	break;
	
case ERROR_INVALID_DRIVE :
	szString = "The system cannot find the drive specified." ;
	break;
	
case ERROR_INVALID_DWP_HANDLE :
	szString = "Invalid handle to a multiple-window position structure." ;
	break;
	
case ERROR_INVALID_EA_HANDLE :
	szString = "The specified extended attribute handle is invalid." ;
	break;
	
case ERROR_INVALID_EA_NAME :
	szString = "The specified extended attribute name was invalid." ;
	break;
	
case ERROR_INVALID_EDIT_HEIGHT :
	szString = "Height must be less than 256." ;
	break;
	
case ERROR_INVALID_ENVIRONMENT :
	szString = "The environment specified is invalid." ;
	break;
	
case ERROR_INVALID_EVENT_COUNT :
	szString = "The number of specified semaphore events for DosMuxSemWait is not correct." ;
	break;
	
case ERROR_INVALID_EVENTNAME :
	szString = "The format of the specified event name is invalid." ;
	break;
	
case ERROR_INVALID_EXE_SIGNATURE :
	szString = "Cannot run %1 in Windows NT mode." ;
	break;
	
case ERROR_INVALID_FILTER_PROC :
	szString = "Invalid hook procedure." ;
	break;
	
case ERROR_INVALID_FLAG_NUMBER :
	szString = "The flag passed is not correct." ;
	break;
	
case ERROR_INVALID_FLAGS :
	szString = "Invalid flags." ;
	break;
	
case ERROR_INVALID_FORM_NAME :
	szString = "The specified form name is invalid." ;
	break;
	
case ERROR_INVALID_FORM_SIZE :
	szString = "The specified form size is invalid." ;
	break;
	
case ERROR_INVALID_FUNCTION :
	szString = "Incorrect function." ;
	break;
	
case ERROR_INVALID_GROUP_ATTRIBUTES :
	szString = "The specified attributes are invalid, or incompatible with the attributes for the group as a whole." ;
	break;
	
case ERROR_INVALID_GROUPNAME :
	szString = "The format of the specified group name is invalid." ;
	break;
	
case ERROR_INVALID_GW_COMMAND :
	szString = "Invalid GW_* command." ;
	break;
	
case ERROR_INVALID_HANDLE :
	szString = "The handle is invalid." ;
	break;
	
case ERROR_INVALID_HOOK_FILTER :
	szString = "Invalid hook procedure type." ;
	break;
	
case ERROR_INVALID_HOOK_HANDLE :
	szString = "Invalid hook handle." ;
	break;
	
case ERROR_INVALID_ICON_HANDLE :
	szString = "Invalid icon handle." ;
	break;
	
case ERROR_INVALID_ID_AUTHORITY :
	szString = "The value provided was an invalid value for an identifier authority." ;
	break;
	
case ERROR_INVALID_INDEX :
	szString = "Invalid index." ;
	break;
	
case ERROR_INVALID_KEYBOARD_HANDLE :
	szString = "Invalid keyboard layout handle." ;
	break;
	
case ERROR_INVALID_LB_MESSAGE :
	szString = "Invalid message for single-selection list box." ;
	break;
	
case ERROR_INVALID_LEVEL :
	szString = "The system call level is not correct." ;
	break;
	
case ERROR_INVALID_LIST_FORMAT :
	szString = "The DosMuxSemWait list is not correct." ;
	break;
	
case ERROR_INVALID_LOGON_HOURS :
	szString = "Logon failure: account logon time restriction violation." ;
	break;
	
case ERROR_INVALID_LOGON_TYPE :
	szString = "A logon request contained an invalid logon type value." ;
	break;
	
case ERROR_INVALID_MEMBER :
	szString = "A new member could not be added to a local group because the member has the wrong account type." ;
	break;
	
case ERROR_INVALID_MENU_HANDLE :
	szString = "Invalid menu handle." ;
	break;
	
case ERROR_INVALID_MESSAGE :
	szString = "The window cannot act on the sent message." ;
	break;
	
case ERROR_INVALID_MESSAGEDEST :
	szString = "The format of the specified message destination is invalid." ;
	break;
	
case ERROR_INVALID_MESSAGENAME :
	szString = "The format of the specified message name is invalid." ;
	break;
	
case ERROR_INVALID_MINALLOCSIZE :
	szString = "The operating system cannot run %1." ;
	break;
	
case ERROR_INVALID_MODULETYPE :
	szString = "The operating system cannot run %1." ;
	break;
	
case ERROR_INVALID_MSGBOX_STYLE :
	szString = "Invalid message box style." ;
	break;
	
case ERROR_INVALID_NAME :
	szString = "The filename, directory name, or volume label syntax is incorrect." ;
	break;
	
case ERROR_INVALID_NETNAME :
	szString = "The format of the specified network name is invalid." ;
	break;
	
case ERROR_INVALID_ORDINAL :
	szString = "The operating system cannot run %1." ;
	break;
	
case ERROR_INVALID_OWNER :
	szString = "This security ID may not be assigned as the owner of this object." ;
	break;
	
case ERROR_INVALID_PARAMETER :
	szString = "The parameter is incorrect." ;
	break;
	
case ERROR_INVALID_PASSWORD :
	szString = "The specified network password is not correct." ;
	break;
	
case ERROR_INVALID_PASSWORDNAME :
	szString = "The format of the specified password is invalid." ;
	break;
	
case ERROR_INVALID_PIXEL_FORMAT :
	szString = "The pixel format is invalid." ;
	break;
	
case ERROR_INVALID_PRIMARY_GROUP :
	szString = "This security ID may not be assigned as the primary group of an object." ;
	break;
	
case ERROR_INVALID_PRINT_MONITOR :
	szString = "The specified print monitor does not have the required functions." ;
	break;
	
case ERROR_INVALID_PRINTER_COMMAND :
	szString = "The printer command is invalid." ;
	break;
	
case ERROR_INVALID_PRINTER_NAME :
	szString = "The printer name is invalid." ;
	break;
	
case ERROR_INVALID_PRINTER_STATE :
	szString = "The state of the printer is invalid." ;
	break;
	
case ERROR_INVALID_PRIORITY :
	szString = "The specified priority is invalid." ;
	break;
	
case ERROR_INVALID_SCROLLBAR_RANGE :
	szString = "Scroll bar range cannot be greater than 0x7FFF." ;
	break;
	
case ERROR_INVALID_SECURITY_DESCR :
	szString = "The security descriptor structure is invalid." ;
	break;
	
case ERROR_INVALID_SEGDPL :
	szString = "The operating system cannot run %1." ;
	break;
	
case ERROR_INVALID_SEGMENT_NUMBER :
	szString = "The system detected a segment number that was not correct." ;
	break;
	
case ERROR_INVALID_SEPARATOR_FILE :
	szString = "The specified separator file is invalid." ;
	break;
	
case ERROR_INVALID_SERVER_STATE :
	szString = "The security account manager (SAM) or local security authority (LSA) server was in the wrong state to perform the security operation." ;
	break;
	
case ERROR_INVALID_SERVICE_ACCOUNT :
	szString = "The account name is invalid or does not exist." ;
	break;
	
case ERROR_INVALID_SERVICE_CONTROL :
	szString = "The requested control is not valid for this service." ;
	break;
	
case ERROR_INVALID_SERVICE_LOCK :
	szString = "The specified service database lock is invalid." ;
	break;
	
case ERROR_INVALID_SERVICENAME :
	szString = "The format of the specified service name is invalid." ;
	break;
	
case ERROR_INVALID_SHARENAME :
	szString = "The format of the specified share name is invalid." ;
	break;
	
case ERROR_INVALID_SHOWWIN_COMMAND :
	szString = "Cannot show or remove the window in the way specified." ;
	break;
	
case ERROR_INVALID_SID :
	szString = "The security ID structure is invalid." ;
	break;
	
case ERROR_INVALID_SIGNAL_NUMBER :
	szString = "The signal being posted is not correct." ;
	break;
	
case ERROR_INVALID_SPI_VALUE :
	szString = "Invalid system-wide (SPI_*) parameter." ;
	break;
	
case ERROR_INVALID_STACKSEG :
	szString = "The operating system cannot run %1." ;
	break;
	
case ERROR_INVALID_STARTING_CODESEG :
	szString = "The operating system cannot run %1." ;
	break;
	
case ERROR_INVALID_SUB_AUTHORITY :
	szString = "The subauthority part of a security ID is invalid for this particular use." ;
	break;
	
case ERROR_INVALID_TARGET_HANDLE :
	szString = "The target internal file identifier is incorrect." ;
	break;
	
case ERROR_INVALID_THREAD_ID :
	szString = "Invalid thread identifier." ;
	break;
	
case ERROR_INVALID_TIME :
	szString = "The specified time is invalid." ;
	break;
	
case ERROR_INVALID_USER_BUFFER :
	szString = "The supplied user buffer is not valid for the requested operation." ;
	break;
	
case ERROR_INVALID_VERIFY_SWITCH :
	szString = "The verify-on-write switch parameter value is not correct." ;
	break;
	
case ERROR_INVALID_WINDOW_HANDLE :
	szString = "Invalid window handle." ;
	break;
	
case ERROR_INVALID_WINDOW_STYLE :
	szString = "The window style or class attribute is invalid for this operation." ;
	break;
	
case ERROR_INVALID_WORKSTATION :
	szString = "Logon failure: user not allowed to log on to this computer." ;
	break;
	
case ERROR_IO_DEVICE :
	szString = "The request could not be performed because of an I/O device error." ;
	break;
	
case ERROR_IO_INCOMPLETE :
	szString = "Overlapped I/O event is not in a signaled state." ;
	break;
	
case ERROR_IO_PENDING :
	szString = "Overlapped I/O operation is in progress." ;
	break;
	
case ERROR_IOPL_NOT_ENABLED :
	szString = "The operating system is not presently configured to run this application." ;
	break;
	
case ERROR_IRQ_BUSY :
	szString = "Unable to open a device that was sharing an interrupt request (IRQ) with other devices. At least one other device that uses that IRQ was already opened." ;
	break;
	
case ERROR_IS_JOIN_PATH :
	szString = "Not enough resources are available to process this command." ;
	break;
	
case ERROR_IS_JOIN_TARGET :
	szString = "A JOIN or SUBST command cannot be used for a drive that contains previously joined drives." ;
	break;
	
case ERROR_IS_JOINED :
	szString = "An attempt was made to use a JOIN or SUBST command on a drive that has already been joined." ;
	break;
	
case ERROR_IS_SUBST_PATH :
	szString = "The path specified is being used in a substitute." ;
	break;
	
case ERROR_IS_SUBST_TARGET :
	szString = "An attempt was made to join or substitute a drive for which a directory on the drive is the target of a previous substitute." ;
	break;
	
case ERROR_IS_SUBSTED :
	szString = "An attempt was made to use a JOIN or SUBST command on a drive that has already been substituted." ;
	break;
	
case ERROR_ITERATED_DATA_EXCEEDS_64k :
	szString = "The operating system cannot run %1." ;
	break;
	
case ERROR_JOIN_TO_JOIN :
	szString = "The system tried to join a drive to a directory on a joined drive." ;
	break;
	
case ERROR_JOIN_TO_SUBST :
	szString = "The system tried to join a drive to a directory on a substituted drive." ;
	break;
	
case ERROR_JOURNAL_HOOK_SET :
	szString = "The journal hook procedure is already installed." ;
	break;
	
case ERROR_KEY_DELETED :
	szString = "Illegal operation attempted on a registry key that has been marked for deletion." ;
	break;
	
case ERROR_KEY_HAS_CHILDREN :
	szString = "Cannot create a symbolic link in a registry key that already has subkeys or values." ;
	break;
	
case ERROR_LABEL_TOO_LONG :
	szString = "The volume label you entered exceeds the label character limit of the target file system." ;
	break;
	
case ERROR_LAST_ADMIN :
	szString = "The last remaining administration account cannot be disabled or deleted." ;
	break;
	
case ERROR_LB_WITHOUT_TABSTOPS :
	szString = "This list box does not support tab stops." ;
	break;
	
case ERROR_LICENSE_QUOTA_EXCEEDED :
	szString = "The service being accessed is licensed for a particular number of connections. No more connections can be made to the service at this time because there are already as many connections as the service can accept." ;
	break;
	
case ERROR_LISTBOX_ID_NOT_FOUND :
	szString = "The list box identifier was not found." ;
	break;
	
case ERROR_LM_CROSS_ENCRYPTION_REQUIRED :
	szString = "A cross-encrypted password is necessary to change this user password." ;
	break;
	
case ERROR_LOCAL_USER_SESSION_KEY :
	szString = "No encryption key is available. A well-known encryption key was returned." ;
	break;
	
case ERROR_LOCK_FAILED :
	szString = "Unable to lock a region of a file." ;
	break;
	
case ERROR_LOCK_VIOLATION :
	szString = "The process cannot access the file because another process has locked a portion of the file." ;
	break;
	
case ERROR_LOCKED :
	szString = "The segment is locked and cannot be reallocated." ;
	break;
	
case ERROR_LOG_FILE_FULL :
	szString = "The event log file is full." ;
	break;
	
case ERROR_LOGIN_TIME_RESTRICTION :
	szString = "Attempting to log in during an unauthorized time of day for this account." ;
	break;
	
case ERROR_LOGIN_WKSTA_RESTRICTION :
	szString = "The account is not authorized to log in from this station." ;
	break;
	
case ERROR_LOGON_FAILURE :
	szString = "Logon failure: unknown user name or bad password." ;
	break;
	
case ERROR_LOGON_NOT_GRANTED :
	szString = "Logon failure: the user has not been granted the requested logon type at this computer." ;
	break;
	
case ERROR_LOGON_SESSION_COLLISION :
	szString = "The logon session ID is already in use." ;
	break;
	
case ERROR_LOGON_SESSION_EXISTS :
	szString = "Cannot start a new logon session with an ID that is already in use." ;
	break;
	
case ERROR_LOGON_TYPE_NOT_GRANTED :
	szString = "Logon failure: the user has not been granted the requested logon type at this computer." ;
	break;
	
case ERROR_LUIDS_EXHAUSTED :
	szString = "No more local user identifiers (LUIDs) are available." ;
	break;
	
case ERROR_MAPPED_ALIGNMENT :
	szString = "The base address or the file offset specified does not have the proper alignment." ;
	break;
	
case ERROR_MAX_THRDS_REACHED :
	szString = "No more threads can be created in the system." ;
	break;
	
case ERROR_MEDIA_CHANGED :
	szString = "The media in the drive may have changed." ;
	break;
	
case ERROR_MEMBER_IN_ALIAS :
	szString = "The specified account name is already a member of the local group." ;
	break;
	
case ERROR_MEMBER_IN_GROUP :
	szString = "Either the specified user account is already a member of the specified group, or the specified group cannot be deleted because it contains a member." ;
	break;
	
case ERROR_MEMBER_NOT_IN_ALIAS :
	szString = "The specified account name is not a member of the local group." ;
	break;
	
case ERROR_MEMBER_NOT_IN_GROUP :
	szString = "The specified user account is not a member of the specified group account." ;
	break;
	
case ERROR_MEMBERS_PRIMARY_GROUP :
	szString = "The user cannot be removed from a group because the group is currently the user's primary group." ;
	break;
	
case ERROR_MENU_ITEM_NOT_FOUND :
	szString = "A menu item was not found." ;
	break;
	
case ERROR_META_EXPANSION_TOO_LONG :
	szString = "The global filename characters, * or ?, are entered incorrectly or too many global filename characters are specified." ;
	break;
	
case ERROR_METAFILE_NOT_SUPPORTED :
	szString = "The requested metafile operation is not supported." ;
	break;
	
case ERROR_MOD_NOT_FOUND :
	szString = "The specified module could not be found." ;
	break;
	
case ERROR_MORE_DATA :
	szString = "More data is available." ;
	break;
	
case ERROR_MORE_WRITES :
	szString = "A serial I/O operation was completed by another write to the serial port. The IOCTL_SERIAL_XOFF_COUNTER reached zero.)" ;
	break;
	
case ERROR_MR_MID_NOT_FOUND :
	szString = "The system cannot find message text for message number 0x%1 in the message file for %2." ;
	break;
	
case ERROR_NEGATIVE_SEEK :
	szString = "An attempt was made to move the file pointer before the beginning of the file." ;
	break;
	
case ERROR_NESTING_NOT_ALLOWED :
	szString = "Can't nest calls to LoadModule." ;
	break;
	
case ERROR_NET_WRITE_FAULT :
	szString = "A write fault occurred on the network." ;
	break;
	
case ERROR_NETLOGON_NOT_STARTED :
	szString = "An attempt was made to logon, but the network logon service was not started." ;
	break;
	
case ERROR_NETNAME_DELETED :
	szString = "The specified network name is no longer available." ;
	break;
	
case ERROR_NETWORK_ACCESS_DENIED :
	szString = "Network access is denied." ;
	break;
	
case ERROR_NETWORK_BUSY :
	szString = "The network is busy." ;
	break;
	
case ERROR_NETWORK_UNREACHABLE :
	szString = "The remote network is not reachable by the transport." ;
	break;
	
case ERROR_NO_ASSOCIATION :
	szString = "No application is associated with the specified file for this operation." ;
	break;
	
case ERROR_NO_BROWSER_SERVERS_FOUND :
	szString = "The list of servers for this workgroup is not currently available" ;
	break;
	
case ERROR_NO_DATA :
	szString = "The pipe is being closed." ;
	break;
	
case ERROR_NO_DATA_DETECTED :
	szString = "No more data is on the tape." ;
	break;
	
case ERROR_NO_IMPERSONATION_TOKEN :
	szString = "An attempt has been made to operate on an impersonation token by a thread that is not currently impersonating a client." ;
	break;
	
case ERROR_NO_INHERITANCE :
	szString = "Indicates an ACL contains no inheritable components." ;
	break;
	
case ERROR_NO_LOG_SPACE :
	szString = "System could not allocate the required space in a registry log." ;
	break;
	
case ERROR_NO_LOGON_SERVERS :
	szString = "There are currently no logon servers available to service the logon request." ;
	break;
	
case ERROR_NO_MEDIA_IN_DRIVE :
	szString = "No media in drive." ;
	break;
	
case ERROR_NO_MORE_DEVICES :
	szString = "No more local devices." ;
	break;
	
case ERROR_NO_MORE_FILES :
	szString = "There are no more files." ;
	break;
	
case ERROR_NO_MORE_ITEMS :
	szString = "No more data is available." ;
	break;
	
case ERROR_NO_MORE_SEARCH_HANDLES :
	szString = "No more internal file identifiers available." ;
	break;
	
case ERROR_NO_NET_OR_BAD_PATH :
	szString = "No network provider accepted the given network path." ;
	break;
	
case ERROR_NO_NETWORK :
	szString = "The network is not present or not started." ;
	break;
	
case ERROR_NO_PROC_SLOTS :
	szString = "The system cannot start another process at this time." ;
	break;
	
case ERROR_NO_QUOTAS_FOR_ACCOUNT :
	szString = "No system quota limits are specifically set for this account." ;
	break;
	
case ERROR_NO_SCROLLBARS :
	szString = "The window does not have scroll bars." ;
	break;
	
case ERROR_NO_SECURITY_ON_OBJECT :
	szString = "Unable to perform a security operation on an object that has no associated security." ;
	break;
	
case ERROR_NO_SHUTDOWN_IN_PROGRESS :
	szString = "Unable to abort the system shutdown because no shutdown was in progress." ;
	break;
	
case ERROR_NO_SIGNAL_SENT :
	szString = "No process in the command subtree has a signal handler." ;
	break;
	
case ERROR_NO_SPOOL_SPACE :
	szString = "Space to store the file waiting to be printed is not available on the server." ;
	break;
	
case ERROR_NO_SUCH_ALIAS :
	szString = "The specified local group does not exist." ;
	break;
	
case ERROR_NO_SUCH_DOMAIN :
	szString = "The specified domain did not exist." ;
	break;
	
case ERROR_NO_SUCH_GROUP :
	szString = "The specified group does not exist." ;
	break;
	
case ERROR_NO_SUCH_LOGON_SESSION :
	szString = "A specified logon session does not exist. It may already have been terminated." ;
	break;
	
case ERROR_NO_SUCH_MEMBER :
	szString = "A new member could not be added to a local group because the member does not exist." ;
	break;
	
case ERROR_NO_SUCH_PACKAGE :
	szString = "A specified authentication package is unknown." ;
	break;
	
case ERROR_NO_SUCH_PRIVILEGE :
	szString = "A specified privilege does not exist." ;
	break;
	
case ERROR_NO_SUCH_USER :
	szString = "The specified user does not exist." ;
	break;
	
case ERROR_NO_SYSTEM_MENU :
	szString = "The window does not have a system menu." ;
	break;
	
case ERROR_NO_SYSTEM_RESOURCES :
	szString = "Insufficient system resources exist to complete the requested service." ;
	break;
	
case ERROR_NO_TOKEN :
	szString = "An attempt was made to reference a token that does not exist." ;
	break;
	
case ERROR_NO_TRUST_LSA_SECRET :
	szString = "The workstation does not have a trust secret." ;
	break;
	
case ERROR_NO_TRUST_SAM_ACCOUNT :
	szString = "The SAM database on the Windows NT Server does not have a computer account for this workstation trust relationship." ;
	break;
	
case ERROR_NO_UNICODE_TRANSLATION :
	szString = "No mapping for the Unicode character exists in the target multibyte code page." ;
	break;
	
case ERROR_NO_USER_SESSION_KEY :
	szString = "There is no user session key for the specified logon session." ;
	break;
	
case ERROR_NO_VOLUME_LABEL :
	szString = "The disk has no volume label." ;
	break;
	
case ERROR_NO_WILDCARD_CHARACTERS :
	szString = "No wildcards were found." ;
	break;
	
case ERROR_NOACCESS :
	szString = "Invalid access to memory location." ;
	break;
	
case ERROR_NOLOGON_INTERDOMAIN_TRUST_ACCOUNT :
	szString = "The account used is an interdomain trust account. Use your global user account or local user account to access this server." ;
	break;
	
case ERROR_NOLOGON_SERVER_TRUST_ACCOUNT :
	szString = "The account used is a server trust account. Use your global user account or local user account to access this server." ;
	break;
	
case ERROR_NOLOGON_WORKSTATION_TRUST_ACCOUNT :
	szString = "The account used is a computer account. Use your global user account or local user account to access this server." ;
	break;
	
case ERROR_NON_MDICHILD_WINDOW :
	szString = "Cannot process a message from a window that is not a multiple document interface (MDI) window." ;
	break;
	
case ERROR_NONE_MAPPED :
	szString = "No mapping between account names and security IDs was done." ;
	break;
	
case ERROR_NONPAGED_SYSTEM_RESOURCES :
	szString = "Insufficient system resources exist to complete the requested service." ;
	break;
	
case ERROR_NOT_ALL_ASSIGNED :
	szString = "Not all privileges referenced are assigned to the caller." ;
	break;
	
case ERROR_NOT_AUTHENTICATED :
	szString = "The operation being requested was not performed because the user has not been authenticated." ;
	break;
	
case ERROR_NOT_CHILD_WINDOW :
	szString = "The window is not a child window." ;
	break;
	
case ERROR_NOT_CONNECTED :
	szString = "This network connection does not exist." ;
	break;
	
case ERROR_NOT_CONTAINER :
	szString = "Cannot enumerate a noncontainer." ;
	break;
	
case ERROR_NOT_DOS_DISK :
	szString = "The specified disk or diskette cannot be accessed." ;
	break;
	
case ERROR_NOT_ENOUGH_MEMORY :
	szString = "Not enough storage is available to process this command." ;
	break;
	
case ERROR_NOT_ENOUGH_QUOTA :
	szString = "Not enough quota is available to process this command." ;
	break;
	
case ERROR_NOT_ENOUGH_SERVER_MEMORY :
	szString = "Not enough server storage is available to process this command." ;
	break;
	
case ERROR_NOT_JOINED :
	szString = "The system tried to delete the JOIN of a drive that is not joined." ;
	break;
	
case ERROR_NOT_LOCKED :
	szString = "The segment is already unlocked." ;
	break;
	
case ERROR_NOT_LOGGED_ON :
	szString = "The operation being requested was not performed because the user has not logged on to the network. The specified service does not exist." ;
	break;
	
case ERROR_NOT_LOGON_PROCESS :
	szString = "The requested action is restricted for use by logon processes only. The calling process has not registered as a logon process." ;
	break;
	
case ERROR_NOT_OWNER :
	szString = "Attempt to release mutex not owned by caller." ;
	break;
	
case ERROR_NOT_READY :
	szString = "The device is not ready." ;
	break;
	
case ERROR_NOT_REGISTRY_FILE :
	szString = "The system has attempted to load or restore a file into the registry, but the specified file is not in a registry file format." ;
	break;
	
case ERROR_NOT_SAME_DEVICE :
	szString = "The system cannot move the file to a different disk drive." ;
	break;
	
case ERROR_NOT_SUBSTED :
	szString = "The system tried to delete the substitution of a drive that is not substituted." ;
	break;
	
case ERROR_NOT_SUPPORTED :
	szString = "The network request is not supported." ;
	break;
	
case ERROR_NOTIFY_ENUM_DIR :
	szString = "A notify change request is being completed and the information is not being returned in the caller's buffer. The caller now needs to enumerate the files to find the changes." ;
	break;
	
case ERROR_NT_CROSS_ENCRYPTION_REQUIRED :
	szString = "A cross-encrypted password is necessary to change a user password." ;
	break;
	
case ERROR_NULL_LM_PASSWORD :
	szString = "The password is too complex to be converted to a LAN Manager password. The LAN Manager password returned is a NULL string." ;
	break;
	
case ERROR_OLD_WIN_VERSION :
	szString = "The specified program requires a newer version of Windows." ;
	break;
	
case ERROR_OPEN_FAILED :
	szString = "The system cannot open the device or file specified." ;
	break;
	
case ERROR_OPEN_FILES :
	szString = "This network connection has files open or requests pending." ;
	break;
	
case ERROR_OPERATION_ABORTED :
	szString = "The I/O operation has been aborted because of either a thread exit or an application request." ;
	break;
	
case ERROR_OUT_OF_PAPER :
	szString = "The printer is out of paper." ;
	break;
	
case ERROR_OUT_OF_STRUCTURES :
	szString = "Storage to process this request is not available." ;
	break;
	
case ERROR_OUTOFMEMORY :
	szString = "Not enough storage is available to complete this operation." ;
	break;
	
case ERROR_PAGED_SYSTEM_RESOURCES :
	szString = "Insufficient system resources exist to complete the requested service." ;
	break;
	
case ERROR_PAGEFILE_QUOTA :
	szString = "Insufficient quota to complete the requested service." ;
	break;
	
case ERROR_PARTIAL_COPY :
	szString = "Only part of a ReadProcessMemory or WriteProcessMemory request was completed." ;
	break;
	
case ERROR_PARTITION_FAILURE :
	szString = "Tape could not be partitioned." ;
	break;
	
case ERROR_PASSWORD_EXPIRED :
	szString = "Logon failure: the specified account password has expired." ;
	break;
	
case ERROR_PASSWORD_MUST_CHANGE :
	szString = "The user must change his password before he logs on the first time." ;
	break;
	
case ERROR_PASSWORD_RESTRICTION :
	szString = "Unable to update the password because a password update rule has been violated." ;
	break;
	
case ERROR_PATH_BUSY :
	szString = "The path specified cannot be used at this time." ;
	break;
	
case ERROR_PATH_NOT_FOUND :
	szString = "The system cannot find the path specified." ;
	break;
	
case ERROR_PIPE_BUSY :
	szString = "All pipe instances are busy." ;
	break;
	
case ERROR_PIPE_CONNECTED :
	szString = "There is a process on other end of the pipe." ;
	break;
	
case ERROR_PIPE_LISTENING :
	szString = "Waiting for a process to open the other end of the pipe." ;
	break;
	
case ERROR_PIPE_NOT_CONNECTED :
	szString = "No process is on the other end of the pipe." ;
	break;
	
case ERROR_POPUP_ALREADY_ACTIVE :
	szString = "Popup menu already active." ;
	break;
	
case ERROR_PORT_UNREACHABLE :
	szString = "No service is operating at the destination network endpoint on the remote system." ;
	break;
	
case ERROR_POSSIBLE_DEADLOCK :
	szString = "A potential deadlock condition has been detected." ;
	break;
	
case ERROR_PRINT_CANCELLED :
	szString = "Your file waiting to be printed was deleted." ;
	break;
	
case ERROR_PRINT_MONITOR_ALREADY_INSTALLED :
	szString = "The specified print monitor has already been installed." ;
	break;
	
case ERROR_PRINT_MONITOR_IN_USE :
	szString = "The specified print monitor is currently in use." ;
	break;
	
case ERROR_PRINT_PROCESSOR_ALREADY_INSTALLED :
	szString = "The specified print processor has already been installed." ;
	break;
	
case ERROR_PRINTER_ALREADY_EXISTS :
	szString = "The printer already exists." ;
	break;
	
case ERROR_PRINTER_DELETED :
	szString = "The specified printer has been deleted." ;
	break;
	
case ERROR_PRINTER_DRIVER_ALREADY_INSTALLED :
	szString = "The specified printer driver is already installed." ;
	break;
	
case ERROR_PRINTER_DRIVER_IN_USE :
	szString = "The specified printer driver is currently in use." ;
	break;
	
case ERROR_PRINTER_HAS_JOBS_QUEUED :
	szString = "The requested operation is not allowed when there are jobs queued to the printer." ;
	break;
	
case ERROR_PRINTQ_FULL :
	szString = "The printer queue is full." ;
	break;
	
case ERROR_PRIVATE_DIALOG_INDEX :
	szString = "Using private DIALOG window words." ;
	break;
	
case ERROR_PRIVILEGE_NOT_HELD :
	szString = "A required privilege is not held by the client." ;
	break;
	
case ERROR_PROC_NOT_FOUND :
	szString = "The specified procedure could not be found." ;
	break;
	
case ERROR_PROCESS_ABORTED :
	szString = "The process terminated unexpectedly." ;
	break;
	
case ERROR_PROTOCOL_UNREACHABLE :
	szString = "The remote system does not support the transport protocol." ;
	break;
	
case ERROR_READ_FAULT :
	szString = "The system cannot read from the specified device." ;
	break;
	
case ERROR_REC_NON_EXISTENT :
	szString = "The name does not exist in the WINS database." ;
	break;
	
case ERROR_REDIR_PAUSED :
	szString = "The specified printer or disk device has been paused." ;
	break;
	
case ERROR_REDIRECTOR_HAS_OPEN_HANDLES :
	szString = "The redirector is in use and cannot be unloaded." ;
	break;
	
case ERROR_REGISTRY_CORRUPT :
	szString = "The registry is corrupted. The structure of one of the files that contains registry data is corrupted, or the system's image of the file in memory is corrupted, or the file could not be recovered because the alternate copy or log was absent or corrupted." ;
	break;
	
case ERROR_REGISTRY_IO_FAILED :
	szString = "An I/O operation initiated by the registry failed unrecoverably. The registry could not read in, or write out, or flush, one of the files that contain the system's image of the registry." ;
	break;
	
case ERROR_REGISTRY_RECOVERED :
	szString = "One of the files in the registry database had to be recovered by use of a log or alternate copy. The recovery was successful." ;
	break;
	
case ERROR_RELOC_CHAIN_XEEDS_SEGLIM :
	szString = "The operating system cannot run %1." ;
	break;
	
case ERROR_REM_NOT_LIST :
	szString = "The remote computer is not available." ;
	break;
	
case ERROR_REMOTE_SESSION_LIMIT_EXCEEDED :
	szString = "An attempt was made to establish a session to a network server, but there are already too many sessions established to that server." ;
	break;
	
case ERROR_REQ_NOT_ACCEP :
	szString = "No more connections can be made to this remote computer at this time because there are already as many connections as the computer can accept." ;
	break;
	
case ERROR_REQUEST_ABORTED :
	szString = "The request was aborted." ;
	break;
	
case ERROR_REQUIRES_INTERACTIVE_WINDOWSTATION :
	szString = "This operation requires an interactive window station." ;
	break;
	
case ERROR_RESOURCE_DATA_NOT_FOUND :
	szString = "The specified image file did not contain a resource section." ;
	break;
	
case ERROR_RESOURCE_LANG_NOT_FOUND :
	szString = "The specified resource language ID cannot be found in the image file." ;
	break;
	
case ERROR_RESOURCE_NAME_NOT_FOUND :
	szString = "The specified resource name cannot be found in the image file." ;
	break;
	
case ERROR_RESOURCE_TYPE_NOT_FOUND :
	szString = "The specified resource type cannot be found in the image file." ;
	break;
	
case ERROR_RETRY :
	szString = "The operation could not be completed. A retry should be performed." ;
	break;
	
case ERROR_REVISION_MISMATCH :
	szString = "Indicates two revision levels are incompatible." ;
	break;
	
case ERROR_RING2_STACK_IN_USE :
	szString = "The ring 2 stack is in use." ;
	break;
	
case ERROR_RING2SEG_MUST_BE_MOVABLE :
	szString = "The code segment cannot be greater than or equal to 64K." ;
	break;
	
case ERROR_RMODE_APP :
	szString = "The specified program was written for an earlier version of Windows." ;
	break;
	
case ERROR_RPL_NOT_ALLOWED :
	szString = "Replication with a nonconfigured partner is not allowed." ;
	break;
	
case ERROR_RXACT_COMMIT_FAILURE :
	szString = "An internal security database corruption has been encountered." ;
	break;
	
case ERROR_RXACT_INVALID_STATE :
	szString = "The transaction state of a registry subtree is incompatible with the requested operation." ;
	break;
	
case ERROR_SAME_DRIVE :
	szString = "The system cannot join or substitute a drive to or for a directory on the same drive." ;
	break;
	
case ERROR_SCREEN_ALREADY_LOCKED :
	szString = "Screen already locked." ;
	break;
	
case ERROR_SECRET_TOO_LONG :
	szString = "The length of a secret exceeds the maximum length allowed." ;
	break;
	
case ERROR_SECTOR_NOT_FOUND :
	szString = "The drive cannot find the sector requested." ;
	break;
	
case ERROR_SEEK :
	szString = "The drive cannot locate a specific area or track on the disk." ;
	break;
	
case ERROR_SEEK_ON_DEVICE :
	szString = "The file pointer cannot be set on the specified device or file." ;
	break;
	
case ERROR_SEM_IS_SET :
	szString = "The semaphore is set and cannot be closed." ;
	break;
	
case ERROR_SEM_NOT_FOUND :
	szString = "The specified system semaphore name was not found." ;
	break;
	
case ERROR_SEM_OWNER_DIED :
	szString = "The previous ownership of this semaphore has ended." ;
	break;
	
case ERROR_SEM_TIMEOUT :
	szString = "The semaphore time-out period has expired." ;
	break;
	
case ERROR_SEM_USER_LIMIT :
	szString = "Insert the diskette for drive %1." ;
	break;
	
case ERROR_SERIAL_NO_DEVICE :
	szString = "No serial device was successfully initialized. The serial driver will unload." ;
	break;
	
case ERROR_SERVER_DISABLED :
	szString = "The server is currently disabled." ;
	break;
	
case ERROR_SERVER_HAS_OPEN_HANDLES :
	szString = "The server is in use and cannot be unloaded." ;
	break;
	
case ERROR_SERVER_NOT_DISABLED :
	szString = "The server is currently enabled." ;
	break;
	
case ERROR_SERVICE_ALREADY_RUNNING :
	szString = "An instance of the service is already running." ;
	break;
	
case ERROR_SERVICE_CANNOT_ACCEPT_CTRL :
	szString = "The service cannot accept control messages at this time." ;
	break;
	
case ERROR_SERVICE_DATABASE_LOCKED :
	szString = "The service database is locked." ;
	break;
	
case ERROR_SERVICE_DEPENDENCY_DELETED :
	szString = "The dependency service does not exist or has been marked for deletion." ;
	break;
	
case ERROR_SERVICE_DEPENDENCY_FAIL :
	szString = "The dependency service or group failed to start." ;
	break;
	
case ERROR_SERVICE_DISABLED :
	szString = "The specified service is disabled and cannot be started." ;
	break;
	
case ERROR_SERVICE_DOES_NOT_EXIST :
	szString = "The specified service does not exist as an installed service." ;
	break;
	
case ERROR_SERVICE_EXISTS :
	szString = "The specified service already exists." ;
	break;
	
case ERROR_SERVICE_LOGON_FAILED :
	szString = "The service did not start due to a logon failure." ;
	break;
	
case ERROR_SERVICE_MARKED_FOR_DELETE :
	szString = "The specified service has been marked for deletion." ;
	break;
	
case ERROR_SERVICE_NEVER_STARTED :
	szString = "No attempts to start the service have been made since the last boot." ;
	break;
	
case ERROR_SERVICE_NO_THREAD :
	szString = "A thread could not be created for the service." ;
	break;
	
case ERROR_SERVICE_NOT_ACTIVE :
	szString = "The service has not been started." ;
	break;
	
case ERROR_SERVICE_NOT_FOUND :
	szString = "The specified service does not exist." ;
	break;
	
case ERROR_SERVICE_REQUEST_TIMEOUT :
	szString = "The service did not respond to the start or control request in a timely fashion." ;
	break;
	
case ERROR_SERVICE_SPECIFIC_ERROR :
	szString = "The service has returned a service-specific error code." ;
	break;
	
case ERROR_SERVICE_START_HANG :
	szString = "After starting, the service hung in a start-pending state." ;
	break;
	
case ERROR_SESSION_CREDENTIAL_CONFLICT :
	szString = "The credentials supplied conflict with an existing set of credentials." ;
	break;
	
case ERROR_SET_POWER_STATE_FAILED :
	szString = "The system BIOS failed an attempt to change the system power state." ;
	break;
	
case ERROR_SET_POWER_STATE_VETOED :
	szString = "An attempt to change the system power state was vetoed by another application or driver." ;
	break;
	
case ERROR_SETCOUNT_ON_BAD_LB :
	szString = "LB_SETCOUNT sent to non-lazy list box." ;
	break;
	
case ERROR_SETMARK_DETECTED :
	szString = "A tape access reached the end of a set of files." ;
	break;
	
case ERROR_SHARING_BUFFER_EXCEEDED :
	szString = "Too many files opened for sharing." ;
	break;
	
case ERROR_SHARING_PAUSED :
	szString = "The remote server has been paused or is in the process of being started." ;
	break;
	
case ERROR_SHARING_VIOLATION :
	szString = "The process cannot access the file because it is being used by another process." ;
	break;
	
case ERROR_SHUTDOWN_IN_PROGRESS :
	szString = "A system shutdown is in progress." ;
	break;
	
case ERROR_SIGNAL_PENDING :
	szString = "A signal is already pending." ;
	break;
	
case ERROR_SIGNAL_REFUSED :
	szString = "The recipient process has refused the signal." ;
	break;
	
case ERROR_SINGLE_INSTANCE_APP :
	szString = "Cannot start more than one instance of the specified program." ;
	break;
	
case ERROR_SOME_NOT_MAPPED :
	szString = "Some mapping between account names and security IDs was not done." ;
	break;
	
case ERROR_SPECIAL_ACCOUNT :
	szString = "Cannot perform this operation on built-in accounts." ;
	break;
	
case ERROR_SPECIAL_GROUP :
	szString = "Cannot perform this operation on this built-in special group." ;
	break;
	
case ERROR_SPECIAL_USER :
	szString = "Cannot perform this operation on this built-in special user." ;
	break;
	
case ERROR_SPL_NO_ADDJOB :
	szString = "An AddJob call was not issued." ;
	break;
	
case ERROR_SPL_NO_STARTDOC :
	szString = "A StartDocPrinter call was not issued." ;
	break;
	
case ERROR_SPOOL_FILE_NOT_FOUND :
	szString = "The spool file was not found." ;
	break;
	
case ERROR_STACK_OVERFLOW :
	szString = "Recursion too deep; the stack overflowed." ;
	break;
	
case ERROR_STATIC_INIT :
	szString = "The importation from the file failed." ;
	break;
	
case ERROR_SUBST_TO_JOIN :
	szString = "The system tried to SUBST a drive to a directory on a joined drive." ;
	break;
	
case ERROR_SUBST_TO_SUBST :
	szString = "The system tried to substitute a drive to a directory on a substituted drive." ;
	break;
	
case ERROR_SUCCESS :
	szString = "The operation completed successfully." ;
	break;
	
case ERROR_SUCCESS_REBOOT_REQUIRED :
	szString = "The requested operation is successful. Changes will not be effective until the system is rebooted." ;
	break;
	
case ERROR_SUCCESS_RESTART_REQUIRED :
	szString = "The requested operation is successful. Changes will not be effective until the service is restarted." ;
	break;
	
case ERROR_SWAPERROR :
	szString = "Error performing inpage operation." ;
	break;
	
case ERROR_SYSTEM_TRACE :
	szString = "System trace information was not specified in your CONFIG.SYS file, or tracing is disallowed." ;
	break;
	
case ERROR_THREAD_1_INACTIVE :
	szString = "The signal handler cannot be set." ;
	break;
	
case ERROR_TIMEOUT :
	szString = "This operation returned because the time-out period expired." ;
	break;
	
case ERROR_TLW_WITH_WSCHILD :
	szString = "Cannot create a top-level child window." ;
	break;
	
case ERROR_TOKEN_ALREADY_IN_USE :
	szString = "The token is already in use as a primary token." ;
	break;
	
case ERROR_TOO_MANY_CMDS :
	szString = "The network BIOS command limit has been reached." ;
	break;
	
case ERROR_TOO_MANY_CONTEXT_IDS :
	szString = "During a logon attempt, the user's security context accumulated too many security IDs." ;
	break;
	
case ERROR_TOO_MANY_LINKS :
	szString = "An attempt was made to create more links on a file than the file system supports." ;
	break;
	
case ERROR_TOO_MANY_LUIDS_REQUESTED :
	szString = "Too many local user identifiers (LUIDs) were requested at one time." ;
	break;
	
case ERROR_TOO_MANY_MODULES :
	szString = "Too many dynamic-link modules are attached to this program or dynamic-link module." ;
	break;
	
case ERROR_TOO_MANY_MUXWAITERS :
	szString = "DosMuxSemWait did not execute; too many semaphores are already set." ;
	break;
	
case ERROR_TOO_MANY_NAMES :
	szString = "The name limit for the local computer network adapter card was exceeded." ;
	break;
	
case ERROR_TOO_MANY_OPEN_FILES :
	szString = "The system cannot open the file." ;
	break;
	
case ERROR_TOO_MANY_POSTS :
	szString = "Too many posts were made to a semaphore." ;
	break;
	
case ERROR_TOO_MANY_SECRETS :
	szString = "The maximum number of secrets that may be stored in a single system has been exceeded." ;
	break;
	
case ERROR_TOO_MANY_SEM_REQUESTS :
	szString = "The semaphore cannot be set again." ;
	break;
	
case ERROR_TOO_MANY_SEMAPHORES :
	szString = "Cannot create another system semaphore." ;
	break;
	
case ERROR_TOO_MANY_SESS :
	szString = "The network BIOS session limit was exceeded." ;
	break;
	
case ERROR_TOO_MANY_SIDS :
	szString = "Too many security IDs have been specified." ;
	break;
	
case ERROR_TOO_MANY_TCBS :
	szString = "Cannot create another thread." ;
	break;
	
case ERROR_TRANSFORM_NOT_SUPPORTED :
	szString = "The requested transformation operation is not supported." ;
	break;
	
case ERROR_TRUST_FAILURE :
	szString = "The network logon failed." ;
	break;
	
case ERROR_TRUSTED_DOMAIN_FAILURE :
	szString = "The trust relationship between the primary domain and the trusted domain failed." ;
	break;
	
case ERROR_TRUSTED_RELATIONSHIP_FAILURE :
	szString = "The trust relationship between this workstation and the primary domain failed." ;
	break;
	
case ERROR_UNABLE_TO_LOCK_MEDIA :
	szString = "Unable to lock the media eject mechanism." ;
	break;
	
case ERROR_UNABLE_TO_UNLOAD_MEDIA :
	szString = "Unable to unload the media." ;
	break;
	
case ERROR_UNEXP_NET_ERR :
	szString = "An unexpected network error occurred." ;
	break;
	
case ERROR_UNKNOWN_PORT :
	szString = "The specified port is unknown." ;
	break;
	
case ERROR_UNKNOWN_PRINT_MONITOR :
	szString = "The specified print monitor is unknown." ;
	break;
	
case ERROR_UNKNOWN_PRINTER_DRIVER :
	szString = "The printer driver is unknown." ;
	break;
	
case ERROR_UNKNOWN_PRINTPROCESSOR :
	szString = "The print processor is unknown." ;
	break;
	
case ERROR_UNKNOWN_REVISION :
	szString = "The revision level is unknown." ;
	break;
	
case ERROR_UNRECOGNIZED_MEDIA :
	szString = "The disk media is not recognized. It may not be formatted." ;
	break;
	
case ERROR_UNRECOGNIZED_VOLUME :
	szString = "The volume does not contain a recognized file system. Please make sure that all required file system drivers are loaded and that the volume is not corrupted." ;
	break;
	
case ERROR_USER_EXISTS :
	szString = "The specified user already exists." ;
	break;
	
case ERROR_USER_MAPPED_FILE :
	szString = "The requested operation cannot be performed on a file with a user-mapped section open." ;
	break;
	
case ERROR_VC_DISCONNECTED :
	szString = "The session was canceled." ;
	break;
	
case ERROR_WAIT_NO_CHILDREN :
	szString = "There are no child processes to wait for." ;
	break;
	
case ERROR_WINDOW_NOT_COMBOBOX :
	szString = "The window is not a combo box." ;
	break;
	
case ERROR_WINDOW_NOT_DIALOG :
	szString = "The window is not a valid dialog window." ;
	break;
	
case ERROR_WINDOW_OF_OTHER_THREAD :
	szString = "Invalid window; it belongs to another thread." ;
	break;
	
case ERROR_WINS_INTERNAL :
	szString = "WINS encountered an error while processing the command." ;
	break;
	
case ERROR_WORKING_SET_QUOTA :
	szString = "Insufficient quota to complete the requested service." ;
	break;
	
case ERROR_WRITE_FAULT :
	szString = "The system cannot write to the specified device." ;
	break;
	
case ERROR_WRITE_PROTECT :
	szString = "The media is write protected." ;
	break;
	
case ERROR_WRONG_DISK :
	szString = "The wrong diskette is in the drive. Insert %2 (Volume Serial Number: %3) into drive %1." ;
	break;
	
case ERROR_WRONG_PASSWORD :
	szString = "Unable to update the password. The value provided as the current password is incorrect." ;
	break;
	
case OR_INVALID_OID :
	szString = "The object specified was not found." ;
	break;
	
case OR_INVALID_OXID :
	szString = "The object exporter specified was not found." ;
	break;
	
case OR_INVALID_SET :
	szString = "The object resolver set specified was not found." ;
	break;
	
case RPC_S_ADDRESS_ERROR :
	szString = "An addressing error occurred in the RPC server." ;
	break;
	
case RPC_S_ALREADY_LISTENING :
	szString = "The RPC server is already listening." ;
	break;
	
case RPC_S_ALREADY_REGISTERED :
	szString = "The object universal unique identifier (UUID) has already been registered." ;
	break;
	
case RPC_S_BINDING_HAS_NO_AUTH :
	szString = "The binding does not contain any authentication information." ;
	break;
	
case RPC_S_BINDING_INCOMPLETE :
	szString = "The binding handle does not contain all required information." ;
	break;
	
case RPC_S_CALL_CANCELLED :
	szString = "The server was altered while processing this call." ;
	break;
	
case RPC_S_CALL_FAILED :
	szString = "The remote procedure call failed." ;
	break;
	
case RPC_S_CALL_FAILED_DNE :
	szString = "The remote procedure call failed and did not execute." ;
	break;
	
case RPC_S_CALL_IN_PROGRESS :
	szString = "A remote procedure call is already in progress for this thread." ;
	break;
	
case RPC_S_CANNOT_SUPPORT :
	szString = "The requested operation is not supported." ;
	break;
	
case RPC_S_CANT_CREATE_ENDPOINT :
	szString = "The endpoint cannot be created." ;
	break;
	
case RPC_S_COMM_FAILURE :
	szString = "Communications failure." ;
	break;
	
case RPC_S_DUPLICATE_ENDPOINT :
	szString = "The endpoint is a duplicate." ;
	break;
	
case RPC_S_ENTRY_ALREADY_EXISTS :
	szString = "The entry already exists." ;
	break;
	
case RPC_S_ENTRY_NOT_FOUND :
	szString = "The entry is not found." ;
	break;
	
case RPC_S_FP_DIV_ZERO :
	szString = "A floating-point operation at the RPC server caused a division by zero." ;
	break;
	
case RPC_S_FP_OVERFLOW :
	szString = "A floating-point overflow occurred at the RPC server." ;
	break;
	
case RPC_S_FP_UNDERFLOW :
	szString = "A floating-point underflow occurred at the RPC server." ;
	break;
	
case RPC_S_GROUP_MEMBER_NOT_FOUND :
	szString = "The group member was not found." ;
	break;
	
case RPC_S_INCOMPLETE_NAME :
	szString = "The entry name is incomplete." ;
	break;
	
case RPC_S_INTERFACE_NOT_FOUND :
	szString = "The interface was not found." ;
	break;
	
case RPC_S_INTERNAL_ERROR :
	szString = "An internal error occurred in a remote procedure call (RPC)." ;
	break;
	
case RPC_S_INVALID_AUTH_IDENTITY :
	szString = "The security context is invalid." ;
	break;
	
case RPC_S_INVALID_BINDING :
	szString = "The binding handle is invalid." ;
	break;
	
case RPC_S_INVALID_BOUND :
	szString = "The array bounds are invalid." ;
	break;
	
case RPC_S_INVALID_ENDPOINT_FORMAT :
	szString = "The endpoint format is invalid." ;
	break;
	
case RPC_S_INVALID_NAF_ID :
	szString = "The network address family is invalid." ;
	break;
	
case RPC_S_INVALID_NAME_SYNTAX :
	szString = "The name syntax is invalid." ;
	break;
	
case RPC_S_INVALID_NET_ADDR :
	szString = "The network address is invalid." ;
	break;
	
case RPC_S_INVALID_NETWORK_OPTIONS :
	szString = "The network options are invalid." ;
	break;
	
case RPC_S_INVALID_OBJECT :
	szString = "The object universal unique identifier (UUID) is the nil UUID." ;
	break;
	
case RPC_S_INVALID_RPC_PROTSEQ :
	szString = "The RPC protocol sequence is invalid." ;
	break;
	
case RPC_S_INVALID_STRING_BINDING :
	szString = "The string binding is invalid." ;
	break;
	
case RPC_S_INVALID_STRING_UUID :
	szString = "The string universal unique identifier (UUID) is invalid." ;
	break;
	
case RPC_S_INVALID_TAG :
	szString = "The tag is invalid." ;
	break;
	
case RPC_S_INVALID_TIMEOUT :
	szString = "The time-out value is invalid." ;
	break;
	
case RPC_S_INVALID_VERS_OPTION :
	szString = "The version option is invalid." ;
	break;
	
case RPC_S_MAX_CALLS_TOO_SMALL :
	szString = "The maximum number of calls is too small." ;
	break;
	
case RPC_S_NAME_SERVICE_UNAVAILABLE :
	szString = "The name service is unavailable." ;
	break;
	
case RPC_S_NO_BINDINGS :
	szString = "There are no bindings." ;
	break;
	
case RPC_S_NO_CALL_ACTIVE :
	szString = "There is not a remote procedure call active in this thread." ;
	break;
	
case RPC_S_NO_CONTEXT_AVAILABLE :
	szString = "No security context is available to allow impersonation." ;
	break;
	
case RPC_S_NO_ENDPOINT_FOUND :
	szString = "No endpoint was found." ;
	break;
	
case RPC_S_NO_ENTRY_NAME :
	szString = "The binding does not contain an entry name." ;
	break;
	
case RPC_S_NO_INTERFACES :
	szString = "No interfaces have been registered." ;
	break;
	
case RPC_S_NO_MORE_BINDINGS :
	szString = "There are no more bindings." ;
	break;
	
case RPC_S_NO_MORE_MEMBERS :
	szString = "There are no more members." ;
	break;
	
case RPC_S_NO_PRINC_NAME :
	szString = "No principal name registered." ;
	break;
	
case RPC_S_NO_PROTSEQS :
	szString = "There are no protocol sequences." ;
	break;
	
case RPC_S_NO_PROTSEQS_REGISTERED :
	szString = "No protocol sequences have been registered." ;
	break;
	
case RPC_S_NOT_ALL_OBJS_UNEXPORTED :
	szString = "There is nothing to unexport." ;
	break;
	
case RPC_S_NOT_CANCELLED :
	szString = "Thread is not canceled." ;
	break;
	
case RPC_S_NOT_LISTENING :
	szString = "The RPC server is not listening." ;
	break;
	
case RPC_S_NOT_RPC_ERROR :
	szString = "The error specified is not a valid Windows NT RPC error code." ;
	break;
	
case RPC_S_NOTHING_TO_EXPORT :
	szString = "No interfaces have been exported." ;
	break;
	
case RPC_S_OBJECT_NOT_FOUND :
	szString = "The object universal unique identifier (UUID) was not found." ;
	break;
	
case RPC_S_OUT_OF_RESOURCES :
	szString = "Not enough resources are available to complete this operation." ;
	break;
	
case RPC_S_PROCNUM_OUT_OF_RANGE :
	szString = "The procedure number is out of range." ;
	break;
	
case RPC_S_PROTOCOL_ERROR :
	szString = "A remote procedure call (RPC) protocol error occurred." ;
	break;
	
case RPC_S_PROTSEQ_NOT_FOUND :
	szString = "The RPC protocol sequence was not found." ;
	break;
	
case RPC_S_PROTSEQ_NOT_SUPPORTED :
	szString = "The RPC protocol sequence is not supported." ;
	break;
	
case RPC_S_SEC_PKG_ERROR :
	szString = "A security package specific error occurred." ;
	break;
	
case RPC_S_SEND_INCOMPLETE :
	szString = "Some data remains to be sent in the request buffer." ;
	break;
	
case RPC_S_SERVER_TOO_BUSY :
	szString = "The RPC server is too busy to complete this operation." ;
	break;
	
case RPC_S_SERVER_UNAVAILABLE :
	szString = "The RPC server is unavailable." ;
	break;
	
case RPC_S_STRING_TOO_LONG :
	szString = "The string is too long." ;
	break;
	
case RPC_S_TYPE_ALREADY_REGISTERED :
	szString = "The type universal unique identifier (UUID) has already been registered." ;
	break;
	
case RPC_S_UNKNOWN_AUTHN_LEVEL :
	szString = "The authentication level is unknown." ;
	break;
	
case RPC_S_UNKNOWN_AUTHN_SERVICE :
	szString = "The authentication service is unknown." ;
	break;
	
case RPC_S_UNKNOWN_AUTHN_TYPE :
	szString = "The authentication type is unknown." ;
	break;
	
case RPC_S_UNKNOWN_AUTHZ_SERVICE :
	szString = "The authorization service is unknown." ;
	break;
	
case RPC_S_UNKNOWN_IF :
	szString = "The interface is unknown." ;
	break;
	
case RPC_S_UNKNOWN_MGR_TYPE :
	szString = "The manager type is unknown." ;
	break;
	
case RPC_S_UNSUPPORTED_AUTHN_LEVEL :
	szString = "The requested authentication level is not supported." ;
	break;
	
case RPC_S_UNSUPPORTED_NAME_SYNTAX :
	szString = "The name syntax is not supported." ;
	break;
	
case RPC_S_UNSUPPORTED_TRANS_SYN :
	szString = "The transfer syntax is not supported by the RPC server." ;
	break;
	
case RPC_S_UNSUPPORTED_TYPE :
	szString = "The universal unique identifier (UUID) type is not supported." ;
	break;
	
case RPC_S_UUID_LOCAL_ONLY :
	szString = "A UUID that is valid only on this computer has been allocated." ;
	break;
	
case RPC_S_UUID_NO_ADDRESS :
	szString = "No network address is available to use to construct a universal unique identifier (UUID)." ;
	break;
	
case RPC_S_WRONG_KIND_OF_BINDING :
	szString = "The binding handle is not the correct type." ;
	break;
	
case RPC_S_ZERO_DIVIDE :
	szString = "The RPC server attempted an integer division by zero." ;
	break;
	
case RPC_X_BAD_STUB_DATA :
	szString = "The stub received bad data." ;
	break;
	
case RPC_X_BYTE_COUNT_TOO_SMALL :
	szString = "The byte count is too small." ;
	break;
	
case RPC_X_ENUM_VALUE_OUT_OF_RANGE :
	szString = "The enumeration value is out of range." ;
	break;
	
case RPC_X_INVALID_ES_ACTION :
	szString = "Invalid operation on the encoding/decoding handle." ;
	break;
	
case RPC_X_INVALID_PIPE_OBJECT :
	szString = "The idl pipe object is invalid or corrupted." ;
	break;
	
case RPC_X_INVALID_PIPE_OPERATION :
	szString = "The operation is invalid for a given idl pipe object." ;
	break;
	
case RPC_X_NO_MORE_ENTRIES :
	szString = "The list of RPC servers available for the binding of auto handles has been exhausted." ;
	break;
	
case RPC_X_NULL_REF_POINTER :
	szString = "A null reference pointer was passed to the stub." ;
	break;
	
case RPC_X_SS_CANNOT_GET_CALL_HANDLE :
	szString = "The stub is unable to get the remote procedure call handle." ;
	break;
	
case RPC_X_SS_CHAR_TRANS_OPEN_FAIL :
	szString = "Unable to open the character translation table file." ;
	break;
	
case RPC_X_SS_CHAR_TRANS_SHORT_FILE :
	szString = "The file containing the character translation table has fewer than 512 bytes." ;
	break;
	
case RPC_X_SS_CONTEXT_DAMAGED :
	szString = "The context handle changed during a remote procedure call." ;
	break;
	
case RPC_X_SS_HANDLES_MISMATCH :
	szString = "The binding handles passed to a remote procedure call do not match." ;
	break;
	
case RPC_X_SS_IN_NULL_CONTEXT :
	szString = "A null context handle was passed from the client to the host during a remote procedure call." ;
	break;
	
case RPC_X_WRONG_ES_VERSION :
	szString = "Incompatible version of the serializing package." ;
	break;
	
case RPC_X_WRONG_PIPE_VERSION :
	szString = "The idl pipe version is not supported." ;
	break;
	
case RPC_X_WRONG_STUB_VERSION :
	szString = "Incompatible version of the RPC stub." ;
	break;

default:
	szString = "Unknown MS Error" ;
	break;
}

LONG lSeverity = lError & 3 ;
switch ( lSeverity )
{
case 0 :
	szString = "Success: " + szString ;
	break;
case 1 :
	szString = "Information: " + szString ;
	break;
case 2 :
	szString = "Warning: " + szString ;
	break;
case 3 :
	szString = "Error: " + szString ;
	break;
default :
	break;
}

return szString ;

/*	LONG lFacility ;
	lFacility = lError & 268369920 ;
	switch ( lFacility )
	{
	case FACILITY_NULL :
		szString = "FACILITY_NULL" ;
		break;
	case FACILITY_WINDOWS :
		szString = "FACILITY_WINDOWS" ;
		break;
	case FACILITY_STORAGE :
		szString = "FACILITY_STORAGE" ;
		break;
	case FACILITY_RPC :
		szString = "FACILITY_RPC" ;
		break;
	case FACILITY_SSPI :
		szString = "FACILITY_SSPI" ;
		break;
	case FACILITY_WIN32 :
		szString = "FACILITY_WIN32" ;
		break;
	case FACILITY_CONTROL :
		szString = "FACILITY_CONTROL" ;
		break;
	case FACILITY_INTERNET :
		szString = "FACILITY_INTERNET" ;
		break;
	case FACILITY_ITF :
		szString = "FACILITY_ITF" ;
		break;
	case FACILITY_DISPATCH :
		szString = "FACILITY_DISPATCH" ;
		break;
	case FACILITY_CERT :
		szString = "FACILITY_CERT" ;
		break;
	default :
		szString = "It fucked up!!!" ;
		break;
	}
*/	

}


//////////////////////////////////////////////////////////////////////////////////////////////////
CString & GetNovellError( long lError, CString & szString)
{

switch ( lError )
{

case TRUE:
szString = "Success, no errors" ;
break;

case FALSE:
szString = "Failiure" ;
break;

case INVALID_DEVICE_INDEX:
szString = "0x8821 INVALID_DEVICE_INDEX " ;
break;

case INVALID_CONN_HANDLE:
szString = "0x8822 Invalid connection handle " ;
break;

case INVALID_QUEUE_ID:
szString = "0x8823 INVALID_QUEUE_ID " ;
break;

case INVALID_PDEVICE_HANDLE:
szString = "0x8824 INVALID_PDEVICE_HANDLE " ;
break;

case INVALID_JOB_HANDLE:
szString = "0x8825 INVALID_JOB_HANDLE " ;
break;

case INVALID_ELEMENT_ID:
szString = "0x8826 INVALID_ELEMENT_ID " ;
break;

case VLM_ERROR:
szString = "0x8800 Attach attempted to server with valid, existing connection } " ;
break;

case INVALID_CONNECTION:
szString = "0x8801 Request attempted with invalid or non-attached connection handle" ;
break;

case DRIVE_IN_USE:
szString = "0x8802 DRIVE_IN_USE 2 - OS/2 only" ;
break;

case CANT_ADD_CDS:
szString = "0x8803 Map drive attempted but unable to add new current directory structure" ;
break;

case BAD_DRIVE_BASE:
szString = "0x8804 Map drive attempted with invalid path specification" ;
break;

case NWE_NET_RECEIVE:
szString = "0x8805 Attempt to receive from the selected transport failed" ;
break;

case NWE_NET_UNKNOWN:
szString = "0x8806 Network send attempted with an un-specific network error" ;
break;

case NWE_SERVER_BAD_SLOT:
szString = "0x8807 Server request attempted with invalid server connection slot" ;
break;

case NWE_SERVER_NO_SLOTS:
szString = "0x8808 Attach attempted to server with no connection slots available" ;
break;

case NET_SEND_ERROR:
szString = "0x8809 Attempt on the selected transport failed {CONNECTION_IN_ERROR_STATE}" ;
break;

case NWE_SERVER_NO_ROUTE:
szString = "0x880A Attempted to find route to server where no route exists " ;
break;

case NWE_BAD_LOCAL_TARGET:
szString = "0x880B {BAD_LOCAL_TARGET 11 - OS/2 only }" ;
break;

case NWE_REQ_TOO_MANY_REQ_FRAGS:
szString = "0x880C Attempted request with too many request fragments specified" ;
break;

case CONNECT_LIST_OVERFLOW:
szString = "0x880D Connection list overflow 13" ;
break;

case MORE_DATA_ERROR:
szString = "0x880E {MORE_DATA_ERROR Client-32 } {BUFFER_OVERFLOW }" ;
break;

case NO_CONNECTION_TO_SERVER:
szString = "0x880F Attempt to get connection for a server not connected" ;
break;

case NO_ROUTER_FOUND:
szString = "0x8810 NO_ROUTER_FOUND 16 - OS/2 only" ;
break;

case INVALID_SHELL_CALL:
szString = "0x8811 Attempted function call to non- existent or illegal function" ;
break;

case LIP_RESIZE_ERROR:
szString = "0x8812 {LIP_RESIZE_ERROR Client-32 }{SCAN_COMPLETE }" ;
break;

case UNSUPPORTED_NAME_FORMAT_TYPE:
szString = "0x8813 {UNSUPPORTED_NAME_FORMAT_TYPE } {INVALID_DIR_HANDLE Client-32 }" ;
break;

case NWE_HANDLE_ALREADY_LICENSED:
szString = "0x8814 {HANDLE_ALREADY_LICENSED } {OUT_OF_CLIENT_MEMORY Client-32 }" ;
break;

case PATH_NOT_OURS:
szString = "0x8815 {PATH_NOT_OURS Client-32 } {HANDLE_ALREADY_UNLICENSED }" ;
break;

case NWE_INVALID_NCP_PACKET_LENGTH:
szString = "0x8816 {PATH_IS_PRINT_DEVICE Client-32 } {INVALID_NCP_PACKET_LENGTH }" ;
break;

case PATH_IS_EXCLUDED_DEVICE:
szString = "0x8817 {PATH_IS_EXCLUDED_DEVICE Client-32 } {SETTING_UP_TIMEOUT }" ;
break;

case PATH_IS_INVALID:
szString = "0x8818 {PATH_IS_INVALID Client-32 } {SETTING_SIGNALS }" ;
break;

case NOT_SAME_DEVICE:
szString = "0x8819 {NOT_SAME_DEVICE Client-32 } {SERVER_CONNECTION_LOST }" ;
break;

case NWE_OUT_OF_HEAP_SPACE:
szString = "0x881A OUT_OF_HEAP_SPACE " ;
break;

case INVALID_SERVICE_REQUEST:
szString = "0x881B {INVALID_SERVICE_REQUEST } {INVALID_SEARCH_HANDLE Client-32 }" ;
break;

case INVALID_TASK_NUMBER:
szString = "0x881C {INVALID_TASK_NUMBER } {INVALID_DEVICE_HANDLE Client-32 }" ;
break;

case ALIAS_NOT_FOUND:
szString = "0x8827 ALIAS_NOT_FOUND " ;
break;

case RESOURCE_SUSPENDED:
szString = "0x8828 RESOURCE_SUSPENDED " ;
break;

case INVALID_QUEUE_SPECIFIED:
szString = "0x8829 INVALID_QUEUE_SPECIFIED " ;
break;

case DEVICE_ALREADY_OPEN:
szString = "0x882A DEVICE_ALREADY_OPEN " ;
break;

case JOB_ALREADY_OPEN:
szString = "0x882B JOB_ALREADY_OPEN " ;
break;

case QUEUE_NAME_ID_MISMATCH:
szString = "0x882C QUEUE_NAME_ID_MISMATCH " ;
break;

case JOB_ALREADY_STARTED:
szString = "0x882D JOB_ALREADY_STARTED " ;
break;

case SPECT_DAA_TYPE_NOT_SUPPORTED:
szString = "0x882E SPECT_DAA_TYPE_NOT_SUPPORTED " ;
break;

case INVALID_ENVIR_HANDLE:
szString = "0x882F INVALID_ENVIR_HANDLE " ;
break;

case INVALID_MESSAGE_LENGTH:
szString = "0x881D {INVALID_MESSAGE_LENGTH } {INVALID_SEM_HANDLE Client-32 }" ;
break;

case EA_SCAN_DONE:
szString = "0x881E {EA_SCAN_DONE } {INVALID_CFG_HANDLE Client-32 }" ;
break;

case BAD_CONNECTION_NUMBER:
szString = "0x881F {BAD_CONNECTION_NUMBER } {INVALID_MOD_HANDLE Client-32 }" ;
break;

case NWE_MULT_TREES_NOT_SUPPORTED:
szString = "0x8820 Attempt to open a connection to a DS tree other than the default tree {ASYN_FIRST_PASS }" ;
break;

case NOT_SAME_CONNECTION:
szString = "0x8830 NOT_SAME_CONNECTION 48 - Internal server request attempted accross different server connections" ;
break;

case PRIMARY_CONNECTION_NOT_SET:
szString = "0x8831 PRIMARY_CONNECTION_NOT_SET 49 - Attempt to retrieve default connection with no primary connection set" ;
break;

case NWE_PRN_CAPTURE_NOT_IN_PROGRESS:
szString = "0x8832 {PRINT_CAPTURE_NOT_SET Client-32 } {KEYWORD_NOT_FOUND Client-32 }" ;
break;

case BAD_BUFFER_LENGTH:
szString = "0x8833 INVALID_BUFFER_LENGTH 51 - Used to indicate length which caller requested on a GetDNC or SetDNC was" ;
break;

case NO_USER_NAME:
szString = "0x8834 {NO_USER_NAME 52 } {NWE_USER_NO_NAME 52 }" ;
break;

case NWE_PRN_NO_LOCAL_SPOOLER:
szString = "0x8835 NO_NETWARE_PRINT_SPOOLER 53 - Capture requested without having the local print spooler installed" ;
break;

case INVALID_PARAMETER:
szString = "0x8836 INVALID_PARAMETER 54 - Attempted function with an invalid function parameter specified" ;
break;

case NWE_CFG_OPEN_FAILED:
szString = "0x8837 NWE_CFG_OPEN_FAILED 55 - OS/2 only" ;
break;

case NO_CONFIG_FILE:
szString = "0x8838 NO_CONFIG_FILE 56 - OS/2 only" ;
break;

case NWE_CFG_READ_FAILED:
szString = "0x8839 CONFIG_FILE_READ_FAILED 57 - OS/2 only " ;
break;

case NWE_CFG_LINE_TOO_LONG:
szString = "0x883A CONFIG_LINE_TOO_LONG 58 - OS/2 only " ;
break;

case NWE_CFG_LINES_IGNORED:
szString = "0x883B CONFIG_LINES_IGNORED 59 - OS/2 only" ;
break;

case NOT_MY_RESOURCE:
szString = "0x883C NOT_MY_RESOURCE 60 - Attempted request made with a parameter using foriegn resource" ;
break;

case NWE_DAEMON_INSTALLED:
szString = "0x883D DAEMON_INSTALLED 61 - OS/2 only" ;
break;

case NWE_PRN_SPOOLER_INSTALLED:
szString = "0x883E PRN_SPOOLER_INSTALLED 62 - Attempted load of print spooler with print spooler already installed" ;
break;

case CONNECTION_TABLE_FULL:
szString = "0x883F CONNECTION_TABLE_FULL 63 - Attempted to allocate a connection handle with no more local connectiont" ;
break;

case CONFIG_SECTION_NOT_FOUND:
szString = "0x8840 CONFIG_SECTION_NOT_FOUND 64 - OS/2 only" ;
break;

case INVALID_TRANSPORT_TYPE:
szString = "0x8841 INVALID_TRANSPORT_TYPE 65 - Attempted function on a connection with an invalid transport selected" ;
break;

case NWE_TDS_TAG_IN_USE:
szString = "0x8842 TDS_TAG_IN_USE 66 - OS/2 only " ;
break;

case RESOURCE_ACCESS_DENIED:
szString = "0x885B RESOURCE_ACCESS_DENIED Client-32 " ;
break;

case SVC_ALREADY_REGISTERED:
szString = "0x8880 SVC_ALREADY_REGISTERED Client-32 " ;
break;

case SVC_REGISTRY_FULL:
szString = "0x8881 SVC_REGISTRY_FULL Client-32 " ;
break;

case SVC_NOT_REGISTERED:
szString = "0x8882 SVC_NOT_REGISTERED Client-32 " ;
break;

case OUT_OF_RESOURCES:
szString = "0x8883 OUT_OF_RESOURCES Client-32 " ;
break;

case RESOLVE_SVC_FAILED:
szString = "0x8884 RESOLVE_SVC_FAILED Client-32 " ;
break;

case CONNECT_FAILED:
szString = "0x8885 CONNECT_FAILED Client-32 " ;
break;

case PROTOCOL_NOT_BOUND:
szString = "0x8886 PROTOCOL_NOT_BOUND Client-32 " ;
break;

case AUTHENTICATION_FAILED:
szString = "0x8887 AUTHENTICATION_FAILED Client-32 " ;
break;

case INVALID_AUTHEN_HANDLE:
szString = "0x8888 INVALID_AUTHEN_HANDLE Client-32 " ;
break;

case TDS_OUT_OF_MEMORY:
szString = "0x8843 TDS_OUT_OF_MEMORY 67 - OS/2 only" ;
break;

case TDS_INVALID_TAG:
szString = "0x8844 TDS_INVALID_TAG 68 - Attempted TDS function with invalid tag" ;
break;

case TDS_WRITE_TRUNCATED:
szString = "0x8845 TDS_WRITE_TRUNCATED 69 - Attempted TDS write with buffer that exceeded buffer" ;
break;

case NO_CONNECTION_TO_DS:
szString = "0x8846 {NO_CONNECTION_TO_DS Client-32 } {SERVICE_BUSY 70 - Request made to partially async function in busy state}" ;
break;

case NO_SERVER_ERROR:
szString = "0x8847 SERVER_NOT_FOUND 71 - Attempted connect failed to find any servers responding" ;
break;

case BAD_VLM_ERROR:
szString = "0x8848 {BAD_VLM_ERROR 72 Attempted function call to non-existant or not-loaded overlay" ;
break;

case NETWORK_DRIVE_IN_USE:
szString = "0x8849 NETWORK_DRIVE_IN_USE 73 - Attempted map to network drive that was already mapped" ;
break;

case LOCAL_DRIVE_IN_USE:
szString = "0x884A LOCAL_DRIVE_IN_USE 74 - Attempted map to local drive that was in use" ;
break;

case NO_DRIVES_AVAILABLE:
szString = "0x884B NO_DRIVES_AVAILABLE 75 - Attempted map to next available drive when none were available" ;
break;

case DEVICE_NOT_REDIRECTED:
szString = "0x884C DEVICE_NOT_REDIRECTED 76 - The device is not redirected" ;
break;

case NO_MORE_SFT_ENTRIES:
szString = "0x884D {NO_MORE_SFT_ENTRIES 77 - Maximum number of files was reached } {NWE_FILE_MAX_REACHED 77 " ;
break;

case NWE_UNLOAD_FAILED:
szString = "0x884E {NWE_UNLOAD_FAILED 78 } {UNLOAD_ERROR Attempted unload failed }" ;
break;

case IN_USE_ERROR:
szString = "0x884F IN_USE_ERROR 79 - Attempted re-use of already in use connection entry" ;
break;

case TOO_MANY_REP_FRAGS:
szString = "0x8850 TOO_MANY_REP_FRAGS 80 - Attempted request with too many reply fragments specified" ;
break;

case TABLE_FULL:
szString = "0x8851 {TABLE_FULL 81 - Attempted to add a name into the name table after it was full } {NWE_NAME_TABLE_FULL 81 }" ;
break;

case SOCKET_NOT_OPEN:
szString = "0x8852 SOCKET_NOT_OPEN 82 - Listen was posted on unopened socket" ;
break;

case MEM_MGR_ERROR:
szString = "0x8853 MEM_MGR_ERROR 83 - Attempted enhanced memory operation failed" ;
break;

case NWE_SFT3_ERROR:
szString = "0x8854 NWE_SFT3_ERROR 84 - An SFT3 switch occured mid-transfer" ;
break;

case PREFERRED_NOT_FOUND:
szString = "0x8855 PREFERRED_NOT_FOUND 85 - the preferred directory server was not established but another directory" ;
break;

case DEVICE_NOT_RECOGNIZED:
szString = "0x8856 DEVICE_NOT_RECOGNIZED 86 - used to determine if the device is not used by VISE so pass it on to the" ;
break;

case BAD_NET_TYPE:
szString = "0x8857 BAD_NET_TYPE 87 - the network type (Bind/NDS) does not match the server version" ;
break;

case ERROR_OPENING_FILE:
szString = "0x8858 ERROR_OPENING_FILE 88 - generic open failure error, invalid path, access denied, etc.." ;
break;

case NO_PREFERRED_SPECIFIED:
szString = "0x8859 NO_PREFERRED_SPECIFIED 89 - no preferred name specified" ;
break;

case ERROR_OPENING_SOCKET:
szString = "0x885A {ERROR_OPENING_SOCKET 90 - error opening a socket } {REQUESTER_FAILURE Client-32 }" ;
break;

case NWE_SIGNATURE_LEVEL_CONFLICT:
szString = "0x8861 {NWE_SIGNATURE_LEVEL_CONFLICT } {SIGNATURE_LEVEL_CONFLICT }" ;
break;

case AUTHEN_HANDLE_ALREADY_EXISTS:
szString = "0x8889 AUTHEN_HANDLE_ALREADY_EXISTS Client-32 " ;
break;

case DIFF_OBJECT_ALREADY_AUTHEN:
szString = "0x8890 DIFF_OBJECT_ALREADY_AUTHEN Client-32 " ;
break;

case REQUEST_NOT_SERVICEABLE:
szString = "0x8891 REQUEST_NOT_SERVICEABLE Client-32 " ;
break;

case AUTO_RECONNECT_SO_REBUILD:
szString = "0x8892 AUTO_RECONNECT_SO_REBUILD Client-32 " ;
break;

case AUTO_RECONNECT_RETRY_REQUEST:
szString = "0x8893 AUTO_RECONNECT_RETRY_REQUEST Client-32 " ;
break;

case ASYNC_REQUEST_IN_USE:
szString = "0x8894 ASYNC_REQUEST_IN_USE Client-32 " ;
break;

case ASYNC_REQUEST_CANCELED:
szString = "0x8895 ASYNC_REQUEST_CANCELED Client-32 " ;
break;

case SESS_SVC_ALREADY_REGISTERED:
szString = "0x8896 SESS_SVC_ALREADY_REGISTERED Client-32 " ;
break;

case SESS_SVC_NOT_REGISTERED:
szString = "0x8897 SESS_SVC_NOT_REGISTERED Client-32 " ;
break;

case PREVIOUSLY_AUTHENTICATED:
szString = "0x8899 PREVIOUSLY_AUTHENTICATED Client-32 " ;
break;

case RESOLVE_SVC_PARTIAL:
szString = "0x889A Invalid user name, context name {RESOLVE_SVC_PARTIAL Client-32}" ;
break;

case NO_DEFAULT_SPECIFIED:
szString = "0x889B NO_DEFAULT_SPECIFIED Client-32 " ;
break;

case HOOK_REQUEST_NOT_HANDLED:
szString = "0x889C HOOK_REQUEST_NOT_HANDLED Client-32 " ;
break;

case AUTO_RECONNECT_SO_IGNORE:
szString = "0x889E AUTO_RECONNECT_SO_IGNORE Client-32 " ;
break;

case ASYNC_REQUEST_NOT_IN_USE:
szString = "0x889F ASYNC_REQUEST_NOT_IN_USE Client-32 " ;
break;

case AUTO_RECONNECT_FAILURE:
szString = "0x88A0 AUTO_RECONNECT_FAILURE Client-32 " ;
break;

case NET_ERROR_ABORT_APPLICATION:
szString = "0x88A1 NET_ERROR_ABORT_APPLICATION Client-32 " ;
break;

case NET_ERROR_SUSPEND_APPLICATION:
szString = "0x88A2 NET_ERROR_SUSPEND_APPLICATION Client-32 " ;
break;

case ERR_NO_MORE_ENTRY:
szString = "0x8914 ERR_NO_MORE_ENTRY 020" ;
break;

case NLM_INVALID_CONNECTION:
szString = "0x890a NLM_INVALID_CONNECTION 010" ;
break;

case NWE_NO_LOCK_FOUND:
szString = "0x8862 NWE_NO_LOCK_FOUND OS/2 - process lock on conn handle failed, process ID not recognized" ;
break;

case NWE_LOCK_TABLE_FULL:
szString = "0x8863 LOCK_TABLE_FULL OS/2 - process lock on conn handle failed, process lock table full" ;
break;

case NWE_INVALID_MATCH_DATA:
szString = "0x8864 INVALID_MATCH_DATA" ;
break;

case NWE_MATCH_FAILED:
szString = "0x8865 MATCH_FAILED" ;
break;

case NO_MORE_ENTRIES:
szString = "0x8866 NO_MORE_ENTRIES" ;
break;

case INSUFFICIENT_RESOURCES:
szString = "0x8867 INSUFFICIENT_RESOURCES" ;
break;

case STRING_TRANSLATION_NEEDED:
szString = "0x8868 {STRING_TRANSLATION_NEEDED Client-32 } {STRING_TRANSLATION }" ;
break;

case ACCESS_VIOLATION:
szString = "0x8869 ACCESS_VIOLATION" ;
break;

case NOT_AUTHENTICATED:
szString = "0x886A NOT_AUTHENTICATED}" ;
break;

case INVALID_LEVEL:
szString = "0x886B INVALID_LEVEL" ;
break;

case NWE_RESOURCE_LOCK:
szString = "0x886C RESOURCE_LOCK_ERROR" ;
break;

case NWE_INVALID_NAME_FORMAT:
szString = "0x886D INVALID_NAME_FORMAT" ;
break;

case OBJECT_EXISTS:
szString = "0x886E OBJECT_EXISTS" ;
break;

case NWE_OBJECT_NOT_FOUND:
szString = "0x886F Invalid Tree {OBJECT_NOT_FOUND}" ;
break;

case DOS_INVALID_DRIVE:
szString = "0x000F DOS_INVALID_DRIVE 255 " ;
break;

case UNSUPPORTED_TRAN_TYPE:
szString = "0x8870 UNSUPPORTED_TRAN_TYPE" ;
break;

case NWE_INVALID_STRING_TYPE:
szString = "0x8871 INVALID_STRING_TYPE" ;
break;

case INVALID_OWNER:
szString = "0x8872 INVALID_OWNER" ;
break;

case NWE_UNSUPPORTED_AUTHENTICATOR:
szString = "0x8873 UNSUPPORTED_AUTHENTICATOR" ;
break;

case NWE_IO_PENDING:
szString = "0x8874 IO_PENDING" ;
break;

case NWE_INVALID_DRIVE_NUMBER:
szString = "0x8875 INVALID_DRIVE_NUM" ;
break;

case HOOK_REQUEST_QUEUED:
szString = "0x889D {HOOK_REQUEST_QUEUED Client-32 } {HOOK_REQUEST_BUSY Client-32 }" ;
break;

case NET_ERROR_PASSWORD_HAS_EXPIRED:
szString = "0x88A {PASSWORD_HAS_EXPIRED 5 Client-32 } {NETWORK_INACTIVE 6 Client-32 }" ;
break;

case REPLY_TRUNCATED:
szString = "0x88e6 {REPLY_TRUNCATED 230 NLM} {NWE_REPLY_TRUNCATED 230 NLM}" ;
break;

case NWE_REQUESTER_FAILURE:
szString = "0x88FF {NWE_REQUESTER_FAILURE } {VLM_FAILURE } {SHELL_FAILURE }" ;
break;

case ERR_INSUFFICIENT_SPACE:
szString = "0x8901 ERR_INSUFFICIENT_SPACE 001" ;
break;

case NWE_BUFFER_TOO_SMALL:
szString = "0x8977 ERR_BUFFER_TOO_SMALL 119" ;
break;

case ERR_VOLUME_FLAG_NOT_SET:
szString = "0x8978 VOLUME_FLAG_NOT_SET 120 the service requested, not avail. on the selected vol. }" ;
break;

case ERR_NO_ITEMS_FOUND:
szString = "0x8979 ERR_NO_ITEMS_FOUND 121" ;
break;

case ERR_CONN_ALREADY_TEMP:
szString = "0x897a ERR_CONN_ALREADY_TEMP 122" ;
break;

case NWE_CONN_ALREADY_LOGGED_IN:
szString = "0x897b ERR_CONN_ALREADY_LOGGED_IN 123" ;
break;

case NWE_CONN_NOT_AUTHENTICATED:
szString = "0x897c ERR_CONN_NOT_AUTHENTICATED 124" ;
break;

case ERR_CONN_NOT_LOGGED_IN:
szString = "0x897d ERR_CONN_NOT_LOGGED_IN 125" ;
break;

case NWE_NCP_BOUNDARY_CHECK_FAILED:
szString = "0x897e NCP_BOUNDARY_CHECK_FAILED 126" ;
break;

case NWE_LOCK_WAITING:
szString = "0x897f ERR_LOCK_WAITING 127" ;
break;

case NWE_FILE_IN_USE:
szString = "0x8980 {FILE_IN_USE_ERROR 128 } {ERR_LOCK_FAIL 128 }" ;
break;

case NWE_FILE_NO_HANDLES:
szString = "0x8981 NO_MORE_FILE_HANDLES 129" ;
break;

case NWE_FILE_NO_OPEN_PRIV:
szString = "0x8982 NO_OPEN_PRIVILEGES 130" ;
break;

case IO_ERROR_NETWORK_DISK:
szString = "0x8983 IO_ERROR_NETWORK_DISK 131" ;
break;

case NO_CREATE_PRIVILEGES:
szString = "0x8984 NO_CREATE_PRIVILEGES 132" ;
break;

case NO_CREATE_DELETE_PRIVILEGES:
szString = "0x8985 NO_CREATE_DELETE_PRIVILEGES 133" ;
break;

case NWE_FILE_EXISTS_READ_ONLY:
szString = "0x8986 CREATE_FILE_EXISTS_READ_ONLY 134" ;
break;

case NWE_FILE_WILD_CARDS_IN_NAME:
szString = "0x8987 {CREATE_FILENAME_ERROR 135} {WILD_CARDS_IN_CREATE_FILE_NAME}" ;
break;

case NWE_FILE_INVALID_HANDLE:
szString = "0x8988 INVALID_FILE_HANDLE 136" ;
break;

case NWE_FILE_NO_SRCH_PRIV:
szString = "0x8989 NO_SEARCH_PRIVILEGES 137" ;
break;

case NWE_FILE_NO_DEL_PRIV:
szString = "0x898A NO_DELETE_PRIVILEGES 138" ;
break;

case NO_RENAME_PRIVILEGES:
szString = "0x898B NO_RENAME_PRIVILEGES 139" ;
break;

case NO_MODIFY_PRIVILEGES:
szString = "0x898C NO_MODIFY_PRIVILEGES 140" ;
break;

case NWE_LOGIN_NO_CONN_AVAIL:
szString = "0x89E0 NWE_LOGIN_NO_CONN_AVAIL 224 " ;
break;

case SOME_FILES_AFFECTED_IN_USE:
szString = "0x898D SOME_FILES_AFFECTED_IN_USE 141" ;
break;

case NWE_FILE_NONE_IN_USE:
szString = "0x898E {NWE_FILE_NONE_IN_USE 142 } {NO_FILES_AFFECTED_IN_USE 142 }" ;
break;

case NWE_FILE_SOME_READ_ONLY:
szString = "0x898F SOME_FILES_AFFECTED_READ_ONLY 143" ;
break;

case NWE_FILE_NONE_READ_ONLY:
szString = "0x8990 {NWE_FILE_NONE_READ_ONLY 144 } {NO_FILES_AFFECTED_READ_ONLY 144 }" ;
break;

case SOME_FILES_RENAMED_NAME_EXISTS:
szString = "0x8991 SOME_FILES_RENAMED_NAME_EXISTS 145" ;
break;

case NO_FILES_RENAMED_NAME_EXISTS:
szString = "0x8992 NO_FILES_RENAMED_NAME_EXISTS 146" ;
break;

case NWE_FILE_NO_READ_PRIV:
szString = "0x8993 NO_READ_PRIVILEGES 147" ;
break;

case NO_WRITE_PRIVILEGES_OR_READONLY:
szString = "0x8994 {NO_WRITE_PRIVILEGES_OR_READONLY 148 } {NWE_FILE_READ_ONLY 148 }" ;
break;

case NWE_FILE_DETACHED:
szString = "0x8995 FILE_DETACHED 149" ;
break;

case SERVER_OUT_OF_MEMORY:
szString = "0x8996 {SERVER_OUT_OF_MEMORY} {DIR_TARGET_INVALID} {TARGET_NOT_A_SUBDIRECTORY can be changed later (note written by server people)}" ;
break;

case NO_DISK_SPACE_FOR_SPOOL_FILE:
szString = "0x8997 NO_DISK_SPACE_FOR_SPOOL_FILE 151" ;
break;

case VOLUME_DOES_NOT_EXIST:
szString = "0x8998 {VOLUME_DOES_NOT_EXIST 152 } {NWE_VOL_INVALID 152 }" ;
break;

case NWE_DIR_FULL:
szString = "0x8999 DIRECTORY_FULL 153" ;
break;

case NWE_VOL_RENAMING_ACROSS:
szString = "0x899A RENAMING_ACROSS_VOLUMES 154" ;
break;

case BAD_DIRECTORY_HANDLE:
szString = "0x899B BAD_DIRECTORY_HANDLE 155" ;
break;

case NWE_PATH_INVALID:
szString = "0x899C {INVALID_PATH 156 } {NO_MORE_TRUSTEES 156 }" ;
break;

case NWE_DIRHANDLE_NO_MORE:
szString = "0x899D NO_MORE_DIRECTORY_HANDLES 157" ;
break;

case INVALID_FILENAME:
szString = "0x899E INVALID_FILENAME 158" ;
break;

case NWE_DIR_ACTIVE:
szString = "0x899F DIRECTORY_ACTIVE 159" ;
break;

case DIRECTORY_NOT_EMPTY:
szString = "0x89A0 DIRECTORY_NOT_EMPTY 160" ;
break;

case NWE_DIR_IO_ERROR:
szString = "0x89A1 DIRECTORY_IO_ERROR 161" ;
break;

case NWE_FILE_IO_LOCKED:
szString = "0x89A2 {NWE_FILE_IO_LOCKED 162 } {READ_FILE_WITH_RECORD_LOCKED 162 }" ;
break;

case NWE_TTS_RANSACTION_RESTARTED:
szString = "0x89A3 ERR_TRANSACTION_RESTARTED 163" ;
break;

case NWE_DIR_RENAME_INVALID:
szString = "0x89A4 ERR_RENAME_DIR_INVALID 164" ;
break;

case NWE_FILE_OPENCREAT_MODE_INVALID:
szString = "0x89A5 ERR_INVALID_FILE_OPENCREATE_MODE 165" ;
break;

case NWE_ALREADY_IN_USE:
szString = "0x89A6 ERR_ALREADY_IN_USE 166" ;
break;

case NWE_RESOURCE_TAG_INVALID:
szString = "0x89A7 ERR_INVALID_RESOURCE_TAG 167" ;
break;

case NWE_ACCESS_DENIED:
szString = "0x89A8 ERR_ACCESS_DENIED 168" ;
break;

case NWE_DATA_STREAM_INVALID:
szString = "0x89BE INVALID_DATA_STREAM 190" ;
break;

case INVALID_NAME_SPACE:
szString = "0x89BF INVALID_NAME_SPACE 191" ;
break;

case NWE_ACCTING_NO_PRIV:
szString = "0x89C0 NO_ACCOUNTING_PRIVILEGES 192" ;
break;

case NWE_ACCTING_NO_BALANCE:
szString = "0x89C1 {NWE_ACCTING_NO_BALANCE 193 } {LOGIN_DENIED_NO_ACCOUNT_BALANCE 193 }" ;
break;

case NWE_ACCTING_NO_CREDIT:
szString = "0x89C2 {NWE_ACCTING_NO_CREDIT 194 } {LOGIN_DENIED_NO_CREDIT 194 }" ;
break;

case ERR_TOO_MANY_HOLDS:
szString = "0x89C3 {ERR_TOO_MANY_HOLDS 195 } {NWE_ACCTING_TOO_MANY_HOLDS 195 }" ;
break;

case ACCOUNTING_DISABLED:
szString = "0x89C4 ACCOUNTING_DISABLED 196" ;
break;

case NWE_LOGIN_LOCKOUT:
szString = "0x89C5 {NWE_LOGIN_LOCKOUT 197 } {INTRUDER_DETECTION_LOCK 197 }" ;
break;

case NO_CONSOLE_OPERATOR:
szString = "0x89C6 {NO_CONSOLE_OPERATOR 198 } {NO_CONSOLE_PRIVILEGES 198 }" ;
break;

case NWE_Q_IO_FAILURE:
szString = "0x89D0 ERR_Q_IO_FAILURE 208" ;
break;

case ERR_NO_QUEUE:
szString = "0x89D1 ERR_NO_QUEUE 209" ;
break;

case ERR_NO_Q_SERVER:
szString = "0x89D2 ERR_NO_Q_SERVER 210" ;
break;

case NWE_Q_NO_RIGHTS:
szString = "0x89D3 ERR_NO_Q_RIGHTS 211" ;
break;

case NWE_Q_FULL:
szString = "0x89D4 ERR_Q_FULL 212" ;
break;

case NWE_Q_NO_JOB:
szString = "0x89D5 ERR_NO_Q_JOB 213" ;
break;

case NWE_Q_NO_JOB_RIGHTS:
szString = "0x89D6 {ERR_NO_Q_JOB_RIGHTS 214 } {NWE_PASSWORD_UNENCRYPTED 214 }" ;
break;

case NWE_Q_IN_SERVICE:
szString = "0x89D7 {PASSWORD_NOT_UNIQUE 215 } {ERR_Q_IN_SERVICE 215 }" ;
break;

case NWE_PASSWORD_TOO_SHORT:
szString = "0x89D8 {ERR_Q_NOT_ACTIVE 216 } {PASSWORD_TOO_SHORT 216 }" ;
break;

case ERR_Q_STN_NOT_SERVER:
szString = "0x89D9 {LOGIN_DENIED_NO_CONNECTION} {ERR_MAXIMUM_LOGINS_EXCEEDED} {ERR_Q_STN_NOT_SERVER}" ;
break;

case NWE_LOGIN_UNAUTHORIZED_TIME:
szString = "0x89DA {UNAUTHORIZED_LOGIN_TIME 218 } {ERR_Q_HALTED 218 }" ;
break;

case NWE_Q_MAX_SERVERS:
szString = "0x89DB {UNAUTHORIZED_LOGIN_STATION 219 } {ERR_Q_MAX_SERVERS 219 }" ;
break;

case NWE_ACCT_DISABLED:
szString = "0x89DC ACCOUNT_DISABLED 220" ;
break;

case NWE_PASSWORD_INVALID:
szString = "0x89DE {NWE_PASSWORD_INVALID 222 } {PASSWORD_HAS_EXPIRED_NO_GRACE 222 }" ;
break;

case NWE_PASSWORD_EXPIRED:
szString = "0x89DF PASSWORD_HAS_EXPIRED 223" ;
break;

case NWE_E_NO_MORE_USERS:
szString = "0x89E7 E_NO_MORE_USERS 231" ;
break;

case NWE_BIND_WRITE_TO_GROUP_PROP:
szString = "0x89E8 {WRITE_PROPERTY_TO_GROUP 232 } {NOT_ITEM_PROPERTY 232 }" ;
break;

case NWE_BIND_MEMBER_ALREADY_EXISTS:
szString = "0x89E9 MEMBER_ALREADY_EXISTS 233" ;
break;

case NWE_BIND_NO_SUCH_MEMBER:
szString = "0x89EA BIND_NO_SUCH_MEMBER 234 } {NO_SUCH_MEMBER 234 }" ;
break;

case NWE_BIND_NOT_GROUP_PROP:
szString = "0x89EB {NWE_BIND_NOT_GROUP_PROP 235 } {NOT_GROUP_PROPERTY 235 }" ;
break;

case NWE_BIND_NO_SUCH_SEGMENT:
szString = "0x89EC {NWE_BIND_NO_SUCH_SEGMENT 236 } {NO_SUCH_SEGMENT 236 }" ;
break;

case NWE_BIND_PROP_ALREADY_EXISTS:
szString = "0x89ED {NWE_BIND_PROP_ALREADY_EXISTS 237 } {PROPERTY_ALREADY_EXISTS 237 }" ;
break;

case NWE_BIND_OBJ_ALREADY_EXISTS:
szString = "0x89EE {NWE_BIND_OBJ_ALREADY_EXISTS 238 } {OBJECT_ALREADY_EXISTS 238 }" ;
break;

case NWE_BIND_NAME_INVALID:
szString = "0x89EF {NWE_BIND_NAME_INVALID 239 } {INVALID_NAME 239 }" ;
break;

case NWE_BIND_WILDCARD_INVALID:
szString = "0x89F0 {NWE_BIND_WILDCARD_INVALID 240 } {WILD_CARD_NOT_ALLOWED 240 }" ;
break;

case NWE_BIND_SECURITY_INVALID:
szString = "0x89F1 {NWE_BIND_SECURITY_INVALID 241 } {INVALID_BINDERY_SECURITY 241 }" ;
break;

case NWE_BIND_OBJ_NO_READ_PRIV:
szString = "0x89F2 {NWE_BIND_OBJ_NO_READ_PRIV 242 } {NO_OBJECT_READ_PRIVILEGE 242 }" ;
break;

case NWE_BIND_OBJ_NO_RENAME_PRIV:
szString = "0x89F3 {NWE_BIND_OBJ_NO_RENAME_PRIV 243 } {NO_OBJECT_RENAME_PRIVILEGE 243 }" ;
break;

case NWE_BIND_OBJ_NO_DELETE_PRIV:
szString = "0x89F4 {NWE_BIND_OBJ_NO_DELETE_PRIV 244 } {NO_OBJECT_DELETE_PRIVILEGE 244 }" ;
break;

case NWE_BIND_OBJ_NO_CREATE_PRIV:
szString = "0x89F5 {NWE_BIND_OBJ_NO_CREATE_PRIV 245 } {NO_OBJECT_CREATE_PRIVILEGE 245 }" ;
break;

case NWE_BIND_PROP_NO_DELETE_PRIV:
szString = "0x89F6 {NWE_BIND_PROP_NO_DELETE_PRIV 246 } {NO_PROPERTY_DELETE_PRIVILEGE 246 } {ne NOT_SAME_LOCAL_DRIVE }" ;
break;

case NO_PROPERTY_CREATE_PRIVILEGE:
szString = "0x89F7 {NO_PROPERTY_CREATE_PRIVILEGE 247 } {NWE_BIND_PROP_NO_CREATE_PRIV 247 } {ne TARGET_DRIVE_NOT_LOCAL }" ;
break;

case NWE_NO_FREE_CONN_SLOTS:
szString = "0x89F9 {NO_FREE_CONNECTION_SLOTS 249 } {NO_PROPERTY_READ_PRIVILEGE 249 }" ;
break;

case TEMP_REMAP_ERROR:
szString = "0x89FA {TEMP_REMAP_ERROR 250 } {NO_MORE_SERVER_SLOTS 250 }" ;
break;

case NWE_NCP_NOT_SUPPORTED:
szString = "0x89FB {ERR_NCP_NOT_SUPPORTED 251 } {NO_SUCH_PROPERTY 251 } {INVALID_PARAMETERS 251 }" ;
break;

case NWE_INET_PACKET_REQ_CANCELED:
szString = "0x89FC Invalid user name, or server name {NO_SUCH_OBJECT} {UNKNOWN_FILE_SERVER}" ;
break;

case LOCK_COLLISION:
szString = "0x89FD {LOCK_COLLISION} {BAD_STATION_NUMBER} {INVALID_PACKET_LENGTH} {UNKNOWN_REQUEST} {NWE_CONN_NUM_INVALID}" ;
break;

case SPOOL_DIRECTORY_ERROR:
szString = "0x89FE 254 {SUPERVISOR_HAS_DISABLED_LOGIN } {DIRECTORY_LOCKED } {TIMEOUT_FAILURE } {BINDERY_LOCKED } {TRUSTEE_NOT_FOUND }" ;
break;

case CLOSE_FCB_ERROR:
szString = "0x89FF 255 Invalid password {NO_SUCH_OBJECT PATH_NOT_LOCATABLE NO_FILES_FOUND FILE_NAME_ERROR LOCK_ERROR INVALID_DRIVE_NUMBER HARDWARE_FAILURE}" ;
break;

case ERR_INSUFFICIENT_MEMORY:
szString = "ERR_INSUFFICIENT_MEMORY - DSERR_NO_ALLOC_SPACE - DSERR_TARGET_NOT_A_SUBDIR" ;
break;

case ERR_NOT_ENOUGH_MEMORY:
szString = "ERR_NOT_ENOUGH_MEMORY" ;
break;

case ERR_BAD_KEY:
szString = "ERR_BAD_KEY" ;
break;

case ERR_BAD_CONTEXT:
szString = "ERR_BAD_CONTEXT" ;
break;

case ERR_BUFFER_FULL:
szString = "ERR_BUFFER_FULL" ;
break;

case ERR_LIST_EMPTY:
szString = "ERR_LIST_EMPTY" ;
break;

case ERR_BAD_SYNTAX:
szString = "ERR_BAD_SYNTAX" ;
break;

case ERR_BUFFER_EMPTY:
szString = "ERR_BUFFER_EMPTY" ;
break;

case ERR_BAD_VERB:
szString = "ERR_BAD_VERB" ;
break;

case ERR_EXPECTED_IDENTIFIER:
szString = "ERR_EXPECTED_IDENTIFIER" ;
break;

case ERR_EXPECTED_EQUALS:
szString = "ERR_EXPECTED_EQUALS" ;
break;

case ERR_ATTR_TYPE_EXPECTED:
szString = "ERR_ATTR_TYPE_EXPECTED" ;
break;

case ERR_ATTR_TYPE_NOT_EXPECTED:
szString = "ERR_ATTR_TYPE_NOT_EXPECTED" ;
break;

case ERR_FILTER_TREE_EMPTY:
szString = "ERR_FILTER_TREE_EMPTY" ;
break;

case ERR_INVALID_OBJECT_NAME:
szString = "ERR_INVALID_OBJECT_NAME" ;
break;

case ERR_EXPECTED_RDN_DELIMITER:
szString = "ERR_EXPECTED_RDN_DELIMITER" ;
break;

case ERR_TOO_MANY_TOKENS:
szString = "ERR_TOO_MANY_TOKENS" ;
break;

case ERR_INCONSISTENT_MULTIAVA:
szString = "ERR_INCONSISTENT_MULTIAVA" ;
break;

case ERR_COUNTRY_NAME_TOO_LONG:
szString = "ERR_COUNTRY_NAME_TOO_LONG" ;
break;

case ERR_SYSTEM_ERROR:
szString = "ERR_SYSTEM_ERROR" ;
break;

case ERR_CANT_ADD_ROOT:
szString = "ERR_CANT_ADD_ROOT" ;
break;

case ERR_UNABLE_TO_ATTACH:
szString = "ERR_UNABLE_TO_ATTACH" ;
break;

case ERR_INVALID_HANDLE:
szString = "ERR_INVALID_HANDLE" ;
break;

case ERR_BUFFER_ZERO_LENGTH:
szString = "ERR_BUFFER_ZERO_LENGTH" ;
break;

case ERR_INVALID_REPLICA_TYPE:
szString = "ERR_INVALID_REPLICA_TYPE" ;
break;

case ERR_INVALID_ATTR_SYNTAX:
szString = "ERR_INVALID_ATTR_SYNTAX" ;
break;

case ERR_INVALID_FILTER_SYNTAX:
szString = "ERR_INVALID_FILTER_SYNTAX" ;
break;

case ERR_CONTEXT_CREATION:
szString = "ERR_CONTEXT_CREATION" ;
break;

case ERR_INVALID_UNION_TAG:
szString = "ERR_INVALID_UNION_TAG" ;
break;

case ERR_INVALID_SERVER_RESPONSE:
szString = "ERR_INVALID_SERVER_RESPONSE" ;
break;

case ERR_NULL_POINTER:
szString = "ERR_NULL_POINTER" ;
break;

case ERR_NO_SERVER_FOUND:
szString = "ERR_NO_SERVER_FOUND" ;
break;

case ERR_NO_CONNECTION:
szString = "ERR_NO_CONNECTION" ;
break;

case ERR_RDN_TOO_LONG:
szString = "ERR_RDN_TOO_LONG" ;
break;

case ERR_DUPLICATE_TYPE:
szString = "ERR_DUPLICATE_TYPE" ;
break;

case ERR_DATA_STORE_FAILURE:
szString = "ERR_DATA_STORE_FAILURE" ;
break;

case ERR_NOT_LOGGED_IN:
szString = "ERR_NOT_LOGGED_IN" ;
break;

case ERR_INVALID_PASSWORD_CHARS:
szString = "ERR_INVALID_PASSWORD_CHARS" ;
break;

case ERR_FAILED_SERVER_AUTHENT:
szString = "ERR_FAILED_SERVER_AUTHENT" ;
break;

case ERR_TRANSPORT:
szString = "ERR_TRANSPORT" ;
break;

case ERR_NO_SUCH_SYNTAX:
szString = "ERR_NO_SUCH_SYNTAX" ;
break;

case ERR_INVALID_DS_NAME:
szString = "ERR_INVALID_DS_NAME" ;
break;

case ERR_ATTR_NAME_TOO_LONG:
szString = "ERR_ATTR_NAME_TOO_LONG" ;
break;

case ERR_INVALID_TDS:
szString = "ERR_INVALID_TDS" ;
break;

case ERR_INVALID_DS_VERSION:
szString = "ERR_INVALID_DS_VERSION" ;
break;

case ERR_UNICODE_TRANSLATION:
szString = "ERR_UNICODE_TRANSLATION" ;
break;

case ERR_SCHEMA_NAME_TOO_LONG:
szString = "ERR_SCHEMA_NAME_TOO_LONG" ;
break;

case ERR_UNICODE_FILE_NOT_FOUND:
szString = "ERR_UNICODE_FILE_NOT_FOUND" ;
break;

case ERR_UNICODE_ALREADY_LOADED:
szString = "ERR_UNICODE_ALREADY_LOADED" ;
break;

case ERR_NOT_CONTEXT_OWNER:
szString = "ERR_NOT_CONTEXT_OWNER" ;
break;

case ERR_ATTEMPT_TO_AUTHENTICATE_0:
szString = "ERR_ATTEMPT_TO_AUTHENTICATE_0" ;
break;

case ERR_NO_WRITABLE_REPLICAS:
szString = "ERR_NO_WRITABLE_REPLICAS" ;
break;

case ERR_DN_TOO_LONG:
szString = "ERR_DN_TOO_LONG" ;
break;

case ERR_RENAME_NOT_ALLOWED:
szString = "ERR_RENAME_NOT_ALLOWED" ;
break;

case ERR_NO_SUCH_ENTRY:
szString = "Server may only support bindery mode {ERR_NO_SUCH_ENTRY}" ;
break;

case ERR_NO_SUCH_VALUE:
szString = "ERR_NO_SUCH_VALUE" ;
break;

case ERR_NO_SUCH_ATTRIBUTE:
szString = "ERR_NO_SUCH_ATTRIBUTE" ;
break;

case ERR_NO_SUCH_CLASS:
szString = "ERR_NO_SUCH_CLASS" ;
break;

case ERR_NO_SUCH_PARTITION:
szString = "ERR_NO_SUCH_PARTITION" ;
break;

case ERR_ENTRY_ALREADY_EXISTS:
szString = "ERR_ENTRY_ALREADY_EXISTS" ;
break;

case ERR_NOT_EFFECTIVE_CLASS:
szString = "ERR_NOT_EFFECTIVE_CLASS" ;
break;

case ERR_ILLEGAL_ATTRIBUTE:
szString = "ERR_ILLEGAL_ATTRIBUTE" ;
break;

case ERR_MISSING_MANDATORY:
szString = "ERR_MISSING_MANDATORY" ;
break;

case ERR_ILLEGAL_DS_NAME:
szString = "ERR_ILLEGAL_DS_NAME" ;
break;

case ERR_ILLEGAL_CONTAINMENT:
szString = "ERR_ILLEGAL_CONTAINMENT" ;
break;

case ERR_CANT_HAVE_MULTIPLE_VALUES:
szString = "ERR_CANT_HAVE_MULTIPLE_VALUES" ;
break;

case ERR_SYNTAX_VIOLATION:
szString = "ERR_SYNTAX_VIOLATION" ;
break;

case ERR_DUPLICATE_VALUE:
szString = "ERR_DUPLICATE_VALUE" ;
break;

case ERR_ATTRIBUTE_ALREADY_EXISTS:
szString = "ERR_ATTRIBUTE_ALREADY_EXISTS" ;
break;

case ERR_MAXIMUM_ENTRIES_EXIST:
szString = "ERR_MAXIMUM_ENTRIES_EXIST" ;
break;

case ERR_DATABASE_FORMAT:
szString = "ERR_DATABASE_FORMAT" ;
break;

case ERR_INCONSISTENT_DATABASE:
szString = "ERR_INCONSISTENT_DATABASE" ;
break;

case ERR_INVALID_COMPARISON:
szString = "ERR_INVALID_COMPARISON" ;
break;

case ERR_COMPARISON_FAILED:
szString = "ERR_COMPARISON_FAILED" ;
break;

case ERR_TRANSACTIONS_DISABLED:
szString = "ERR_TRANSACTIONS_DISABLED" ;
break;

case ERR_INVALID_TRANSPORT:
szString = "ERR_INVALID_TRANSPORT" ;
break;

case ERR_SYNTAX_INVALID_IN_NAME:
szString = "ERR_SYNTAX_INVALID_IN_NAME" ;
break;

case ERR_REPLICA_ALREADY_EXISTS:
szString = "ERR_REPLICA_ALREADY_EXISTS" ;
break;

case ERR_TRANSPORT_FAILURE:
szString = "ERR_TRANSPORT_FAILURE" ;
break;

case ERR_ALL_REFERRALS_FAILED:
szString = "ERR_ALL_REFERRALS_FAILED" ;
break;

case ERR_CANT_REMOVE_NAMING_VALUE:
szString = "ERR_CANT_REMOVE_NAMING_VALUE" ;
break;

case ERR_OBJECT_CLASS_VIOLATION:
szString = "ERR_OBJECT_CLASS_VIOLATION" ;
break;

case ERR_ENTRY_IS_NOT_LEAF:
szString = "ERR_ENTRY_IS_NOT_LEAF" ;
break;

case ERR_DIFFERENT_TREE:
szString = "ERR_DIFFERENT_TREE" ;
break;

case ERR_ILLEGAL_REPLICA_TYPE:
szString = "ERR_ILLEGAL_REPLICA_TYPE" ;
break;

case ERR_SYSTEM_FAILURE:
szString = "ERR_SYSTEM_FAILURE" ;
break;

case ERR_INVALID_ENTRY_FOR_ROOT:
szString = "ERR_INVALID_ENTRY_FOR_ROOT" ;
break;

case ERR_NO_REFERRALS:
szString = "ERR_NO_REFERRALS" ;
break;

case ERR_REMOTE_FAILURE:
szString = "ERR_REMOTE_FAILURE" ;
break;

case ERR_UNREACHABLE_SERVER:
szString = "ERR_UNREACHABLE_SERVER" ;
break;

case ERR_PREVIOUS_MOVE_IN_PROGRESS:
szString = "ERR_PREVIOUS_MOVE_IN_PROGRESS" ;
break;

case ERR_NO_CHARACTER_MAPPING:
szString = "ERR_NO_CHARACTER_MAPPING" ;
break;

case ERR_INCOMPLETE_AUTHENTICATION:
szString = "ERR_INCOMPLETE_AUTHENTICATION" ;
break;

case ERR_INVALID_CERTIFICATE:
szString = "ERR_INVALID_CERTIFICATE" ;
break;

case ERR_INVALID_REQUEST:
szString = "ERR_INVALID_REQUEST" ;
break;

case ERR_INVALID_ITERATION:
szString = "ERR_INVALID_ITERATION" ;
break;

case ERR_SCHEMA_IS_NONREMOVABLE:
szString = "ERR_SCHEMA_IS_NONREMOVABLE" ;
break;

case ERR_SCHEMA_IS_IN_USE:
szString = "ERR_SCHEMA_IS_IN_USE" ;
break;

case ERR_CLASS_ALREADY_EXISTS:
szString = "ERR_CLASS_ALREADY_EXISTS" ;
break;

case ERR_BAD_NAMING_ATTRIBUTES:
szString = "ERR_BAD_NAMING_ATTRIBUTES" ;
break;

case ERR_NOT_ROOT_PARTITION:
szString = "ERR_NOT_ROOT_PARTITION" ;
break;

case ERR_INSUFFICIENT_STACK:
szString = "ERR_INSUFFICIENT_STACK" ;
break;

case ERR_INSUFFICIENT_BUFFER:
szString = "ERR_INSUFFICIENT_BUFFER" ;
break;

case ERR_AMBIGUOUS_CONTAINMENT:
szString = "ERR_AMBIGUOUS_CONTAINMENT" ;
break;

case ERR_AMBIGUOUS_NAMING:
szString = "ERR_AMBIGUOUS_NAMING" ;
break;

case ERR_DUPLICATE_MANDATORY:
szString = "ERR_DUPLICATE_MANDATORY" ;
break;

case ERR_DUPLICATE_OPTIONAL:
szString = "ERR_DUPLICATE_OPTIONAL" ;
break;

case ERR_PARTITION_BUSY:
szString = "ERR_PARTITION_BUSY" ;
break;

case ERR_MULTIPLE_REPLICAS:
szString = "ERR_MULTIPLE_REPLICAS" ;
break;

case ERR_CRUCIAL_REPLICA:
szString = "ERR_CRUCIAL_REPLICA" ;
break;

case ERR_SCHEMA_SYNC_IN_PROGRESS:
szString = "ERR_SCHEMA_SYNC_IN_PROGRESS" ;
break;

case ERR_SKULK_IN_PROGRESS:
szString = "ERR_SKULK_IN_PROGRESS" ;
break;

case ERR_TIME_NOT_SYNCHRONIZED:
szString = "ERR_TIME_NOT_SYNCHRONIZED" ;
break;

case ERR_RECORD_IN_USE:
szString = "ERR_RECORD_IN_USE" ;
break;

case ERR_DS_VOLUME_NOT_MOUNTED:
szString = "ERR_DS_VOLUME_NOT_MOUNTED" ;
break;

case ERR_DS_VOLUME_IO_FAILURE:
szString = "ERR_DS_VOLUME_IO_FAILURE" ;
break;

case ERR_DS_LOCKED:
szString = "ERR_DS_LOCKED" ;
break;

case ERR_OLD_EPOCH:
szString = "ERR_OLD_EPOCH" ;
break;

case ERR_NEW_EPOCH:
szString = "ERR_NEW_EPOCH" ;
break;

case ERR_INCOMPATIBLE_DS_VERSION:
szString = "ERR_INCOMPATIBLE_DS_VERSION" ;
break;

case ERR_PARTITION_ROOT:
szString = "ERR_PARTITION_ROOT" ;
break;

case ERR_ENTRY_NOT_CONTAINER:
szString = "ERR_ENTRY_NOT_CONTAINER" ;
break;

case ERR_FAILED_AUTHENTICATION:
szString = "Invalid Password {FAILED_AUTHENTICATION}" ;
break;

case ERR_INVALID_CONTEXT:
szString = "ERR_INVALID_CONTEXT" ;
break;

case ERR_NO_SUCH_PARENT:
szString = "ERR_NO_SUCH_PARENT" ;
break;

case ERR_NO_ACCESS:
szString = "ERR_NO_ACCESS" ;
break;

case ERR_REPLICA_NOT_ON:
szString = "ERR_REPLICA_NOT_ON" ;
break;

case ERR_INVALID_NAME_SERVICE:
szString = "ERR_INVALID_NAME_SERVICE" ;
break;

case ERR_INVALID_TASK:
szString = "ERR_INVALID_TASK" ;
break;

case ERR_INVALID_CONN_HANDLE:
szString = "ERR_INVALID_CONN_HANDLE" ;
break;

case ERR_INVALID_IDENTITY:
szString = "ERR_INVALID_IDENTITY" ;
break;

case ERR_DUPLICATE_ACL:
szString = "ERR_DUPLICATE_ACL" ;
break;

case ERR_PARTITION_ALREADY_EXISTS:
szString = "ERR_PARTITION_ALREADY_EXISTS" ;
break;

case ERR_TRANSPORT_MODIFIED:
szString = "ERR_TRANSPORT_MODIFIED" ;
break;

case ERR_ALIAS_OF_AN_ALIAS:
szString = "ERR_ALIAS_OF_AN_ALIAS" ;
break;

case ERR_AUDITING_FAILED:
szString = "ERR_AUDITING_FAILED" ;
break;

case ERR_INVALID_API_VERSION:
szString = "ERR_INVALID_API_VERSION" ;
break;

case ERR_SECURE_NCP_VIOLATION:
szString = "ERR_SECURE_NCP_VIOLATION" ;
break;

case ERR_MOVE_IN_PROGRESS:
szString = "ERR_MOVE_IN_PROGRESS" ;
break;

case ERR_NOT_LEAF_PARTITION:
szString = "ERR_NOT_LEAF_PARTITION" ;
break;

case ERR_CANNOT_ABORT:
szString = "ERR_CANNOT_ABORT" ;
break;

case ERR_CACHE_OVERFLOW:
szString = "ERR_CACHE_OVERFLOW" ;
break;

case ERR_INVALID_SUBORDINATE_COUNT:
szString = "ERR_INVALID_SUBORDINATE_COUNT" ;
break;

case ERR_INVALID_RDN:
szString = "ERR_INVALID_RDN" ;
break;

case ERR_MOD_TIME_NOT_CURRENT:
szString = "ERR_MOD_TIME_NOT_CURRENT" ;
break;

case ERR_INCORRECT_BASE_CLASS:
szString = "ERR_INCORRECT_BASE_CLASS" ;
break;

case ERR_MISSING_REFERENCE:
szString = "ERR_MISSING_REFERENCE" ;
break;

case ERR_LOST_ENTRY:
szString = "ERR_LOST_ENTRY" ;
break;

case ERR_AGENT_ALREADY_REGISTERED:
szString = "ERR_AGENT_ALREADY_REGISTERED" ;
break;

case ERR_DS_LOADER_BUSY:
szString = "ERR_DS_LOADER_BUSY" ;
break;

case ERR_DS_CANNOT_RELOAD:
szString = "ERR_DS_CANNOT_RELOAD" ;
break;

case ERR_REPLICA_IN_SKULK:
szString = "ERR_REPLICA_IN_SKULK" ;
break;

case ERR_FATAL:
szString = "ERR_FATAL" ;
break;

case ERR_OBSOLETE_API:
szString = "ERR_OBSOLETE_API" ;
break;

case ERR_SYNCHRONIZATION_DISABLED:
szString = "ERR_SYNCHRONIZATION_DISABLED" ;
break;

case ERR_INVALID_PARAMETER:
szString = "ERR_INVALID_PARAMETER" ;
break;

case ERR_DUPLICATE_TEMPLATE:
szString = "ERR_DUPLICATE_TEMPLATE" ;
break;

case ERR_NO_MASTER_REPLICA:
szString = "ERR_NO_MASTER_REPLICA" ;
break;

case ERR_DUPLICATE_CONTAINMENT:
szString = "ERR_DUPLICATE_CONTAINMENT" ;
break;

case ERR_NOT_SIBLING:
szString = "ERR_NOT_SIBLING" ;
break;

case ERR_INVALID_SIGNATURE:
szString = "ERR_INVALID_SIGNATURE" ;
break;

case ERR_INVALID_RESPONSE:
szString = "ERR_INVALID_RESPONSE" ;
break;

case ERR_INSUFFICIENT_SOCKETS:
szString = "ERR_INSUFFICIENT_SOCKETS" ;
break;

case ERR_DATABASE_READ_FAIL:
szString = "ERR_DATABASE_READ_FAIL" ;
break;

case ERR_INVALID_CODE_PAGE:
szString = "ERR_INVALID_CODE_PAGE" ;
break;

case ERR_INVALID_ESCAPE_CHAR:
szString = "ERR_INVALID_ESCAPE_CHAR" ;
break;

case ERR_INVALID_DELIMITERS:
szString = "ERR_INVALID_DELIMITERS" ;
break;

case ERR_NOT_IMPLEMENTED:
szString = "ERR_NOT_IMPLEMENTED" ;
break;

case ERR_CHECKSUM_FAILURE:
szString = "ERR_CHECKSUM_FAILURE" ;
break;

case ERR_CHECKSUMMING_NOT_SUPPORTED:
szString = "ERR_CHECKSUMMING_NOT_SUPPORTED" ;
break;

case ERR_CRC_FAILURE:
szString = "ERR_CRC_FAILURE" ;
break;

case DSERR_BUFFER_TOO_SMALL:
szString = "DSERR_BUFFER_TOO_SMALL" ;
break;

case DSERR_VOLUME_FLAG_NOT_SET:
szString = "DSERR_VOLUME_FLAG_NOT_SET" ;
break;

case DSERR_NO_ITEMS_FOUND:
szString = "DSERR_NO_ITEMS_FOUND" ;
break;

case DSERR_CONN_ALREADY_TEMPORARY:
szString = "DSERR_CONN_ALREADY_TEMPORARY" ;
break;

case DSERR_CONN_ALREADY_LOGGED_IN:
szString = "DSERR_CONN_ALREADY_LOGGED_IN" ;
break;

case DSERR_CONN_NOT_AUTHENTICATED:
szString = "DSERR_CONN_NOT_AUTHENTICATED" ;
break;

case DSERR_CONN_NOT_LOGGED_IN:
szString = "DSERR_CONN_NOT_LOGGED_IN" ;
break;

case DSERR_NCP_BOUNDARY_CHECK_FAILED:
szString = "DSERR_NCP_BOUNDARY_CHECK_FAILED" ;
break;

case DSERR_LOCK_WAITING:
szString = "DSERR_LOCK_WAITING" ;
break;

case DSERR_LOCK_FAIL:
szString = "DSERR_LOCK_FAIL" ;
break;

case DSERR_OUT_OF_HANDLES:
szString = "DSERR_OUT_OF_HANDLES" ;
break;

case DSERR_NO_OPEN_PRIVILEGE:
szString = "DSERR_NO_OPEN_PRIVILEGE" ;
break;

case DSERR_HARD_IO_ERROR:
szString = "DSERR_HARD_IO_ERROR" ;
break;

case DSERR_NO_CREATE_PRIVILEGE:
szString = "DSERR_NO_CREATE_PRIVILEGE" ;
break;

case DSERR_NO_CREATE_DELETE_PRIV:
szString = "DSERR_NO_CREATE_DELETE_PRIV" ;
break;

case DSERR_R_O_CREATE_FILE:
szString = "DSERR_R_O_CREATE_FILE" ;
break;

case DSERR_CREATE_FILE_INVALID_NAME:
szString = "DSERR_CREATE_FILE_INVALID_NAME" ;
break;

case DSERR_INVALID_FILE_HANDLE:
szString = "DSERR_INVALID_FILE_HANDLE" ;
break;

case DSERR_NO_SEARCH_PRIVILEGE:
szString = "DSERR_NO_SEARCH_PRIVILEGE" ;
break;

case DSERR_NO_DELETE_PRIVILEGE:
szString = "DSERR_NO_DELETE_PRIVILEGE" ;
break;

case DSERR_NO_RENAME_PRIVILEGE:
szString = "DSERR_NO_RENAME_PRIVILEGE" ;
break;

case DSERR_NO_SET_PRIVILEGE:
szString = "DSERR_NO_SET_PRIVILEGE" ;
break;

case DSERR_SOME_FILES_IN_USE:
szString = "DSERR_SOME_FILES_IN_USE" ;
break;

case DSERR_ALL_FILES_IN_USE:
szString = "DSERR_ALL_FILES_IN_USE" ;
break;

case DSERR_SOME_READ_ONLY:
szString = "DSERR_SOME_READ_ONLY" ;
break;

case DSERR_ALL_READ_ONLY:
szString = "DSERR_ALL_READ_ONLY" ;
break;

case DSERR_SOME_NAMES_EXIST:
szString = "DSERR_SOME_NAMES_EXIST" ;
break;

case DSERR_ALL_NAMES_EXIST:
szString = "DSERR_ALL_NAMES_EXIST" ;
break;

case DSERR_NO_READ_PRIVILEGE:
szString = "DSERR_NO_READ_PRIVILEGE" ;
break;

case DSERR_NO_WRITE_PRIVILEGE:
szString = "DSERR_NO_WRITE_PRIVILEGE" ;
break;

case DSERR_FILE_DETACHED:
szString = "DSERR_FILE_DETACHED" ;
break;

case DSERR_NO_SPOOL_SPACE:
szString = "DSERR_NO_SPOOL_SPACE" ;
break;

case DSERR_INVALID_VOLUME:
szString = "DSERR_INVALID_VOLUME" ;
break;

case DSERR_DIRECTORY_FULL:
szString = "DSERR_DIRECTORY_FULL" ;
break;

case DSERR_RENAME_ACROSS_VOLUME:
szString = "DSERR_RENAME_ACROSS_VOLUME" ;
break;

case DSERR_BAD_DIR_HANDLE:
szString = "DSERR_BAD_DIR_HANDLE" ;
break;

case DSERR_INVALID_PATH:
szString = "DSERR_INVALID_PATH - DSERR_NO_SUCH_EXTENSION" ;
break;

case DSERR_NO_DIR_HANDLES:
szString = "DSERR_NO_DIR_HANDLES" ;
break;

case DSERR_BAD_FILE_NAME:
szString = "DSERR_BAD_FILE_NAME" ;
break;

case DSERR_DIRECTORY_ACTIVE:
szString = "DSERR_DIRECTORY_ACTIVE" ;
break;

case DSERR_DIRECTORY_NOT_EMPTY:
szString = "DSERR_DIRECTORY_NOT_EMPTY" ;
break;

case DSERR_DIRECTORY_IO_ERROR:
szString = "DSERR_DIRECTORY_IO_ERROR" ;
break;

case DSERR_IO_LOCKED:
szString = "DSERR_IO_LOCKED" ;
break;

case DSERR_TRANSACTION_RESTARTED:
szString = "DSERR_TRANSACTION_RESTARTED" ;
break;

case DSERR_RENAME_DIR_INVALID:
szString = "DSERR_RENAME_DIR_INVALID" ;
break;

case DSERR_INVALID_OPENCREATE_MODE:
szString = "DSERR_INVALID_OPENCREATE_MODE" ;
break;

case DSERR_ALREADY_IN_USE:
szString = "DSERR_ALREADY_IN_USE" ;
break;

case DSERR_INVALID_RESOURCE_TAG:
szString = "DSERR_INVALID_RESOURCE_TAG" ;
break;

case DSERR_ACCESS_DENIED:
szString = "DSERR_ACCESS_DENIED" ;
break;

case DSERR_INVALID_DATA_STREAM:
szString = "DSERR_INVALID_DATA_STREAM" ;
break;

case DSERR_INVALID_NAME_SPACE:
szString = "DSERR_INVALID_NAME_SPACE" ;
break;

case DSERR_NO_ACCOUNTING_PRIVILEGES:
szString = "DSERR_NO_ACCOUNTING_PRIVILEGES" ;
break;

case DSERR_NO_ACCOUNT_BALANCE:
szString = "DSERR_NO_ACCOUNT_BALANCE" ;
break;

case DSERR_CREDIT_LIMIT_EXCEEDED:
szString = "DSERR_CREDIT_LIMIT_EXCEEDED" ;
break;

case DSERR_TOO_MANY_HOLDS:
szString = "DSERR_TOO_MANY_HOLDS" ;
break;

case DSERR_ACCOUNTING_DISABLED:
szString = "DSERR_ACCOUNTING_DISABLED" ;
break;

case DSERR_LOGIN_LOCKOUT:
szString = "DSERR_LOGIN_LOCKOUT" ;
break;

case DSERR_NO_CONSOLE_RIGHTS:
szString = "DSERR_NO_CONSOLE_RIGHTS" ;
break;

case DSERR_Q_IO_FAILURE:
szString = "DSERR_Q_IO_FAILURE" ;
break;

case DSERR_NO_QUEUE:
szString = "DSERR_NO_QUEUE" ;
break;

case DSERR_NO_Q_SERVER:
szString = "DSERR_NO_Q_SERVER" ;
break;

case DSERR_NO_Q_RIGHTS:
szString = "DSERR_NO_Q_RIGHTS" ;
break;

case DSERR_Q_FULL:
szString = "DSERR_Q_FULL" ;
break;

case DSERR_NO_Q_JOB:
szString = "DSERR_NO_Q_JOB" ;
break;

case DSERR_NO_Q_JOB_RIGHTS:
szString = "DSERR_NO_Q_JOB_RIGHTS - DSERR_UNENCRYPTED_NOT_ALLOWED" ;
break;

case DSERR_Q_IN_SERVICE:
szString = "DSERR_DUPLICATE_PASSWORD - DSERR_Q_IN_SERVICE" ;
break;

case DSERR_Q_NOT_ACTIVE:
szString = "DSERR_PASSWORD_TOO_SHORT - DSERR_Q_NOT_ACTIVE" ;
break;

case DSERR_MAXIMUM_LOGINS_EXCEEDED:
szString = "DSERR_MAXIMUM_LOGINS_EXCEEDED - DSERR_Q_STN_NOT_SERVER" ;
break;

case DSERR_Q_HALTED:
szString = "DSERR_BAD_LOGIN_TIME - DSERR_Q_HALTED" ;
break;

case DSERR_Q_MAX_SERVERS:
szString = "DSERR_Q_MAX_SERVERS - DSERR_NODE_ADDRESS_VIOLATION" ;
break;

case DSERR_LOG_ACCOUNT_EXPIRED:
szString = "DSERR_LOG_ACCOUNT_EXPIRED" ;
break;

case DSERR_BAD_PASSWORD:
szString = "DSERR_BAD_PASSWORD" ;
break;

case DSERR_PASSWORD_EXPIRED:
szString = "DSERR_PASSWORD_EXPIRED" ;
break;

case DSERR_NO_LOGIN_CONN_AVAILABLE:
szString = "DSERR_NO_LOGIN_CONN_AVAILABLE" ;
break;

case DSERR_WRITE_TO_GROUP_PROPERTY:
szString = "DSERR_WRITE_TO_GROUP_PROPERTY" ;
break;

case DSERR_MEMBER_ALREADY_EXISTS:
szString = "DSERR_MEMBER_ALREADY_EXISTS" ;
break;

case DSERR_NO_SUCH_MEMBER:
szString = "DSERR_NO_SUCH_MEMBER" ;
break;

case DSERR_PROPERTY_NOT_GROUP:
szString = "DSERR_PROPERTY_NOT_GROUP" ;
break;

case DSERR_NO_SUCH_VALUE_SET:
szString = "DSERR_NO_SUCH_VALUE_SET" ;
break;

case DSERR_PROPERTY_ALREADY_EXISTS:
szString = "DSERR_PROPERTY_ALREADY_EXISTS" ;
break;

case DSERR_OBJECT_ALREADY_EXISTS:
szString = "DSERR_OBJECT_ALREADY_EXISTS" ;
break;

case DSERR_ILLEGAL_NAME:
szString = "DSERR_ILLEGAL_NAME" ;
break;

case DSERR_ILLEGAL_WILDCARD:
szString = "DSERR_ILLEGAL_WILDCARD" ;
break;

case DSERR_BINDERY_SECURITY:
szString = "DSERR_BINDERY_SECURITY" ;
break;

case DSERR_NO_OBJECT_READ_RIGHTS:
szString = "DSERR_NO_OBJECT_READ_RIGHTS" ;
break;

case DSERR_NO_OBJECT_RENAME_RIGHTS:
szString = "DSERR_NO_OBJECT_RENAME_RIGHTS" ;
break;

case DSERR_NO_OBJECT_DELETE_RIGHTS:
szString = "DSERR_NO_OBJECT_DELETE_RIGHTS" ;
break;

case DSERR_NO_OBJECT_CREATE_RIGHTS:
szString = "DSERR_NO_OBJECT_CREATE_RIGHTS" ;
break;

case DSERR_NO_PROPERTY_DELETE_RIGHTS:
szString = "DSERR_NO_PROPERTY_DELETE_RIGHTS" ;
break;

case DSERR_NO_PROPERTY_CREATE_RIGHTS:
szString = "DSERR_NO_PROPERTY_CREATE_RIGHTS" ;
break;

case DSERR_NO_PROPERTY_WRITE_RIGHTS:
szString = "DSERR_NO_PROPERTY_WRITE_RIGHTS" ;
break;

case DSERR_NO_PROPERTY_READ_RIGHTS:
szString = "DSERR_NO_PROPERTY_READ_RIGHTS" ;
break;

case DSERR_TEMP_REMAP:
szString = "DSERR_TEMP_REMAP" ;
break;

case DSERR_UNKNOWN_REQUEST:
szString = "DSERR_UNKNOWN_REQUEST - DSERR_NO_SUCH_PROPERTY" ;
break;

case DSERR_NO_SUCH_OBJECT:
szString = "DSERR_NO_SUCH_OBJECT - DSERR_MESSAGE_QUEUE_FULL - DSERR_TARGET_ALREADY_HAS_MSG" ;
break;

case DSERR_BAD_STATION_NUMBER:
szString = "DSERR_BAD_STATION_NUMBER" ;
break;

case DSERR_DIR_LOCKED:
szString = "DSERR_BINDERY_LOCKED - DSERR_DIR_LOCKED - DSERR_TRUSTEE_NOT_FOUND - DSERR_SPOOL_DELETE" ;
break;

case ERR_OF_SOME_SORT:
szString = "ERR_OF_SOME_SORT - BAD_PARAMETER - FILE_EXISTS - NOT_LOGGED_IN - TARGET_NOT_ACCEPTING_MSGS - NO_FILES_FOUND - HARD_FAILURE - MUST_FORCE_DOWN" ;
break;
 
default:
szString = "Unknown Error, I'm Very, Very, Sorry!" ;
break;

}

return szString ;

}
//////////////////////////////////////////////////////////////////////////////////////////////////


	
