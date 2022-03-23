#include "MachParamsDlg.h"
#include "MachParamsDlgMacros.h"

using namespace New5Axis;

#ifdef MessageBox
#undef MessageBox
#endif

#include "LinkParamsLeadsDlg.h"

void MachParamsDlg::InitializeTabLeadLink()
{/*
		System::Object ^	m_LinkParamsDlg;
		System::Windows::Forms::ImageList^  m_ImageListLeads; //then we use their image list here...

		//int m_LeadLinkZigControlCheckStatesFlags;

	m_LinkParamsDlg = gcnew LinkParamsLeadsDlg(m_ResourceManager,this);
	LinkParamsLeadsDlg ^ m_LinkParamsDlg2 = dynamic_cast<LinkParamsLeadsDlg^>(m_LinkParamsDlg);

	if( m_scLinkParams->UseLeadInFirstEntry )
	{
		if( m_scLinkParams->UseDefaultLeadInFirstEntry )
			*m_LinkParamsDlg2->m_scParamsLeadIn = *m_scLinkParams->LeadInDefault;
		else
			*m_LinkParamsDlg2->m_scParamsLeadIn = *m_scLinkParams->LeadInFirstEntry;
	}
	else
	{
		*m_LinkParamsDlg2->m_scParamsLeadIn = *m_scLinkParams->LeadInDefault;
	}

	if( m_scLinkParams->UseLeadOutLastExit )
	{
		if( m_scLinkParams->UseDefaultLeadOutLastExit )
			*m_LinkParamsDlg2->m_scParamsLeadOut = *m_scLinkParams->LeadOutDefault;
		else
			*m_LinkParamsDlg2->m_scParamsLeadOut = *m_scLinkParams->LeadOutLastExit;
	}
	else
	{
		*m_LinkParamsDlg2->m_scParamsLeadOut = *m_scLinkParams->LeadOutDefault;
	}

	m_LinkParamsDlg2->m_ResourceManager = m_ResourceManager;

	m_ImageListLeads = m_LinkParamsDlg2->m_ImageListLeads;
	m_LinkParamsDlg2->m_formatDblStr = m_formatDblStr;
	m_LinkParamsDlg2->m_pParentDlg = this;

	String ^ title = gcnew String("dummy");
	m_LinkParamsDlg2->SetMode(title,2,false,false);


	*/

	m_LeadInOutTitleDefaults = m_ResourceManager->GetString("IDS_LEADINOUT_TITLE_DEFAULTS");
	m_LeadInOutTitleSmallGaps = m_ResourceManager->GetString("IDS_LEADINOUT_TITLE_SMALL_GAPS");
	m_LeadInOutTitleLargeGaps = m_ResourceManager->GetString("IDS_LEADINOUT_TITLE_LARGE_GAPS");
	m_LeadInOutTitleSmallLinks = m_ResourceManager->GetString("IDS_LEADINOUT_TITLE_SMALL_LINKS");
	m_LeadInOutTitleLargeLinks = m_ResourceManager->GetString("IDS_LEADINOUT_TITLE_LARGE_LINKS");
	m_LeadInOutTitleSmallPasses = m_ResourceManager->GetString("IDS_LEADINOUT_TITLE_SMALL_PASSES");
	m_LeadInOutTitleLargePasses = m_ResourceManager->GetString("IDS_LEADINOUT_TITLE_LARGE_PASSES");
	m_LeadInOutTitleLeadIn = m_ResourceManager->GetString("IDS_LEADINOUT_TITLE_LEAD_IN");
	m_LeadInOutTitleLeadOut = m_ResourceManager->GetString("IDS_LEADINOUT_TITLE_LEAD_OUT");

	double value; 

	GET_COMBOBOX2_RESOURCES6( SmallGaps, MoveHandling )
	GET_COMBOBOX2_RESOURCES6( LargeGaps, MoveHandling )
	GET_COMBOBOX2_RESOURCES6( SmallSlices, MoveHandling )
	GET_COMBOBOX2_RESOURCES6( LargeSlices, MoveHandling )
	GET_COMBOBOX2_RESOURCES6( SmallPasses, MoveHandling )
	GET_COMBOBOX2_RESOURCES6( LargePasses, MoveHandling )

	GET_COMBOBOX2_RESOURCES4( SmallGapsUse, LeadParamsUse )
	GET_COMBOBOX2_RESOURCES4( LargeGapsUse, LeadParamsUse )
	GET_COMBOBOX2_RESOURCES4( SmallSlicesUse, LeadParamsUse )
	GET_COMBOBOX2_RESOURCES4( LargeSlicesUse, LeadParamsUse )
	GET_COMBOBOX2_RESOURCES4( SmallPassesUse, LeadParamsUse )
	GET_COMBOBOX2_RESOURCES4( LargePassesUse, LeadParamsUse )

	GET_COMBOBOX_RESOURCES2( GapSizeSmallGapsType )
	GET_COMBOBOX_RESOURCES2( MoveSizeSmallSlicesType )
	//GET_COMBOBOX2_RESOURCES2( GapSizeSmallGapsType, LeadParamsSizeType )
	//GET_COMBOBOX2_RESOURCES2( MoveSizeSmallSlicesType, LeadParamsSizeType )

	GET_COMBOBOX2_RESOURCES4( LeadInFirstEntry, FirstEntry )
	GET_COMBOBOX2_RESOURCES5( LeadOutLastExit, LastExit )

	if( m_scLinkParams->UseLeadInFirstEntry == true )
	{
		m_cbLeadInFirstEntry->SelectedIndex = static_cast<int>(UITypes::DB2UILinkFirstEntryTypes(m_scLinkParams->LinkFirstEntry));
	}
	else
	{
		m_cbLeadInFirstEntry->SelectedIndex = 0;
	}
	if( m_scLinkParams->UseLeadOutLastExit == true )
	{
		m_cbLeadOutLastExit->SelectedIndex = static_cast<int>(UITypes::DB2UILinkLastExitTypes(m_scLinkParams->LinkLastExit));
	}
	else
	{
		m_cbLeadOutLastExit->SelectedIndex = 0;
	}

	m_cbSmallGaps->SelectedIndex = static_cast<int>(UITypes::DB2UILinkMoveHandlingTypes(m_scLinkParams->GroupGaps->MoveHandlingSmall));
	m_cbLargeGaps->SelectedIndex = static_cast<int>(UITypes::DB2UILinkMoveHandlingTypes(m_scLinkParams->GroupGaps->MoveHandlingLarge));
	m_cbSmallSlices->SelectedIndex = static_cast<int>(UITypes::DB2UILinkMoveHandlingTypes(m_scLinkParams->GroupSlices->MoveHandlingSmall));
	m_cbLargeSlices->SelectedIndex = static_cast<int>(UITypes::DB2UILinkMoveHandlingTypes(m_scLinkParams->GroupSlices->MoveHandlingLarge));
	m_cbSmallPasses->SelectedIndex = static_cast<int>(UITypes::DB2UILinkMoveHandlingTypes(m_scLinkParams->GroupPasses->MoveHandlingSmall));
	m_cbLargePasses->SelectedIndex = static_cast<int>(UITypes::DB2UILinkMoveHandlingTypes(m_scLinkParams->GroupPasses->MoveHandlingLarge));

	m_cbSmallGapsUse->SelectedIndex = static_cast<int>(UITypes::DB2UILeadsParamsUseTypes(m_scLinkParams->GroupGaps->GetLeadParamsUse(true)));
	m_cbLargeGapsUse->SelectedIndex = static_cast<int>(UITypes::DB2UILeadsParamsUseTypes(m_scLinkParams->GroupGaps->GetLeadParamsUse(false)));
	m_cbSmallSlicesUse->SelectedIndex = static_cast<int>(UITypes::DB2UILeadsParamsUseTypes(m_scLinkParams->GroupSlices->GetLeadParamsUse(true)));
	m_cbLargeSlicesUse->SelectedIndex = static_cast<int>(UITypes::DB2UILeadsParamsUseTypes(m_scLinkParams->GroupSlices->GetLeadParamsUse(false)));
	m_cbSmallPassesUse->SelectedIndex = static_cast<int>(UITypes::DB2UILeadsParamsUseTypes(m_scLinkParams->GroupPasses->GetLeadParamsUse(true)));
	m_cbLargePassesUse->SelectedIndex = static_cast<int>(UITypes::DB2UILeadsParamsUseTypes(m_scLinkParams->GroupPasses->GetLeadParamsUse(false)));

	m_cbGapSizeSmallGapsType->SelectedIndex = static_cast<int>(UITypes::DB2UILeadsParamsSizeTypes(m_scLinkParams->GroupGaps->GetLeadsParamsSizeType()));
	m_cbMoveSizeSmallSlicesType->SelectedIndex = static_cast<int>(UITypes::DB2UILeadsParamsSizeTypes(m_scLinkParams->GroupSlices->GetLeadsParamsSizeType()));

	value = m_scLinkParams->GroupGaps->Size;
	m_txtGapSizeSmallGaps->Text = (value.ToString()->Format(m_formatDblStr, (value)));
	value = m_scLinkParams->GroupSlices->Size;
	m_txtMoveSizeSmallSlices->Text = (value.ToString()->Format(m_formatDblStr, (value)));
	value = m_scLinkParams->GroupPasses->Size;
	m_txtMoveSizeSmallPasses->Text = (value.ToString()->Format(m_formatDblStr, (value)));

	(m_scLinkParams->GroupGaps->EnabledLeadParams(true)) ? m_btnLeadsSmallGaps->Enabled = true : m_btnLeadsSmallGaps->Enabled = false;
	(m_scLinkParams->GroupGaps->EnabledLeadParams(false)) ? m_btnLeadsLargeGaps->Enabled = true : m_btnLeadsLargeGaps->Enabled = false;
	(m_scLinkParams->GroupSlices->EnabledLeadParams(true)) ? m_btnLeadsSmallSlices->Enabled = true : m_btnLeadsSmallSlices->Enabled = false;
	(m_scLinkParams->GroupSlices->EnabledLeadParams(false)) ? m_btnLeadsLargeSlices->Enabled = true : m_btnLeadsLargeSlices->Enabled = false;
	(m_scLinkParams->GroupPasses->EnabledLeadParams(true)) ? m_btnLeadsSmallPasses->Enabled = true : m_btnLeadsSmallPasses->Enabled = false;
	(m_scLinkParams->GroupPasses->EnabledLeadParams(false)) ? m_btnLeadsLargePasses->Enabled = true : m_btnLeadsLargePasses->Enabled = false;

	/*if( m_scLinkParams->GroupSlices->UseLeadInSmall )
		m_LeadLinkZigControlCheckStatesFlags |= 1;
	if( m_scLinkParams->GroupSlices->UseLeadOutSmall )
		m_LeadLinkZigControlCheckStatesFlags |= 2;
	if( m_scLinkParams->GroupSlices->UseLeadInLarge )
		m_LeadLinkZigControlCheckStatesFlags |= 4;
	if( m_scLinkParams->GroupSlices->UseLeadOutLarge )
		m_LeadLinkZigControlCheckStatesFlags |= 8;

	UIRetraceTypes retraceType = static_cast<UIRetraceTypes>(m_cbRetraceType->SelectedIndex);
	if (retraceType == UIRetraceTypes::UI_RETRACE_TYPE_ZIG)
	{
		m_cbSmallSlicesUse->Enabled = false;
		m_cbLargeSlicesUse->Enabled = false;

		if( m_scLinkParams->UseLeadInFirstEntry && m_scLinkParams->UseLeadOutLastExit )
		{
			m_cbSmallSlicesUse->SelectedIndex = 2;
			m_cbLargeSlicesUse->SelectedIndex = 2;
		}
		else if( m_scLinkParams->UseLeadInFirstEntry )
		{
			m_cbSmallSlicesUse->SelectedIndex = 0;
			m_cbLargeSlicesUse->SelectedIndex = 0;
		}
		else if( m_scLinkParams->UseLeadOutLastExit )
		{
			m_cbSmallSlicesUse->SelectedIndex = 1;
			m_cbLargeSlicesUse->SelectedIndex = 1;
		}
		else
		{
			m_cbSmallSlicesUse->SelectedIndex = 3;
			m_cbLargeSlicesUse->SelectedIndex = 3;
		}
		if( m_chkSpiralFlag->Checked )
		{
			m_cbSmallSlicesUse->SelectedIndex = 3;
		}
	}
	else
	{
		m_cbSmallSlicesUse->Enabled = true;
		m_cbLargeSlicesUse->Enabled = true;
		if( m_LeadLinkZigControlCheckStatesFlags & 1 && m_LeadLinkZigControlCheckStatesFlags & 2 )
			m_cbSmallSlicesUse->SelectedIndex = 2;
		else if( m_LeadLinkZigControlCheckStatesFlags & 1 )
			m_cbSmallSlicesUse->SelectedIndex = 0;
		else if( m_LeadLinkZigControlCheckStatesFlags & 2 )
			m_cbSmallSlicesUse->SelectedIndex = 1;
		else
			m_cbSmallSlicesUse->SelectedIndex = 3;

		if( m_LeadLinkZigControlCheckStatesFlags & 4 && m_LeadLinkZigControlCheckStatesFlags & 8 )
			m_cbLargeSlicesUse->SelectedIndex = 2;
		else if( m_LeadLinkZigControlCheckStatesFlags & 4 )
			m_cbLargeSlicesUse->SelectedIndex = 0;
		else if( m_LeadLinkZigControlCheckStatesFlags & 8 )
			m_cbLargeSlicesUse->SelectedIndex = 1;
		else
			m_cbLargeSlicesUse->SelectedIndex = 3;
	}*/

	m_cbLeadInFirstEntry->Focus();
	if (m_scMachParams->GetOperationMode() == OperationMode::EDIT_PARAMETERS)
	{
		m_tabLinkParams->Enabled = false;
	}
}
void MachParamsDlg::RefreshTabLeadsLink()
{
	InitializeTabLeadLink();
}

#define CALC_FIRST_ENTRY_IMAGE \
	switch(m_cbLeadInFirstEntry->SelectedIndex) \
	{ \
	case UILinkLastExitTypes::UI_NONE: \
		DisplayBitmap(ID_NONE); \
		break; \
	case UILinkFirstEntryTypes::UI_FROM_RAPID_PLANE: \
		DisplayBitmap(ID_LinkParamsFirstEntryClearanceArea); \
		break; \
	case UILinkFirstEntryTypes::UI_USE_FEED_DISTANCE: \
		DisplayBitmap(ID_LinkParamsFirstEntryFeedDistance); \
		break; \
	case UILinkFirstEntryTypes::UI_USE_RAPID_DISTANCE: \
		DisplayBitmap(ID_LinkParamsFirstEntryRapidDistance); \
		break; \
	}

#define CALC_LAST_EXIT_IMAGE \
	switch(m_cbLeadOutLastExit->SelectedIndex) \
	{ \
	case UILinkLastExitTypes::UI_NONE: \
		DisplayBitmap(ID_NONE); \
		break; \
	case UILinkLastExitTypes::UI_BACK_TO_RAPID_PLANE: \
		DisplayBitmap(ID_LinkParamsLastExitClearanceArea); \
		break; \
	case UILinkLastExitTypes::UI_USE_RAPID_DISTANCE: \
		DisplayBitmap(ID_LinkParamsLastExitRapidDistance); \
		break; \
	case UILinkLastExitTypes::UI_USE_FEED_DISTANCE: \
		DisplayBitmap(ID_LinkParamsLastExitFeedDistance); \
		break; \
	case UILinkLastExitTypes::UI_BACK_TO_CLEARANCE_THROUGH_TUBE_CENTER: \
		DisplayBitmap(ID_LinkParamsLastExitClearanceAreaTubeCenter); \
		break; \
	}

#define CALC_GAP_MOVE_HANDLING_IMAGE( ctrl ) \
	switch(ctrl->SelectedIndex) \
	{ \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_DIRECT: \
		DisplayBitmap(ID_LinkParamsGapsTypeDirect); \
		break; \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_BROKEN_FEED: \
		DisplayBitmap(ID_LinkParamsGapsTypeRetractFeedDistance); \
		break; \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_RAPID_PLANE: \
		DisplayBitmap(ID_LinkParamsGapsTypeRetractClearanceArea); \
		break; \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_FOLLOW_SURFS: \
		DisplayBitmap(ID_LinkParamsGapsTypeFollowSurfaces); \
		break; \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_BLEND_SPLINE: \
		DisplayBitmap(ID_LinkParamsGapsTypeBlendSpline); \
		break; \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_BROKEN_FEED_RAP: \
		DisplayBitmap(ID_LinkParamsGapsTypeRetractRapidDistance); \
		break; \
	}

#define CALC_SLICE_MOVE_HANDLING_IMAGE( ctrl ) \
	switch(ctrl->SelectedIndex) \
	{ \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_DIRECT: \
		DisplayBitmap(ID_LinkParamsSlicesTypeDirect); \
		break; \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_BROKEN_FEED: \
		DisplayBitmap(ID_LinkParamsSlicesTypeRetractFeedDistance); \
		break; \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_RAPID_PLANE: \
		DisplayBitmap(ID_LinkParamsSlicesTypeRetractClearanceArea); \
		break; \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_FOLLOW_SURFS: \
		DisplayBitmap(ID_LinkParamsSlicesTypeFollowSurfaces); \
		break; \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_BLEND_SPLINE: \
		DisplayBitmap(ID_LinkParamsSlicesTypeBlendSpline); \
		break; \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_BROKEN_FEED_RAP: \
		DisplayBitmap(ID_LinkParamsSlicesTypeRetractRapidDistance); \
		break; \
	}

#define CALC_PASS_MOVE_HANDLING_IMAGE( ctrl ) \
	switch(ctrl->SelectedIndex) \
	{ \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_DIRECT: \
		DisplayBitmap(ID_LinkParamsPassesTypeDirect); \
		break; \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_BROKEN_FEED: \
		DisplayBitmap(ID_LinkParamsPassesTypeRetractFeedDistance); \
		break; \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_RAPID_PLANE: \
		DisplayBitmap(ID_LinkParamsPassesTypeRetractClearanceArea); \
		break; \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_FOLLOW_SURFS: \
		DisplayBitmap(ID_LinkParamsPassesTypeFollowSurfaces); \
		break; \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_BLEND_SPLINE: \
		DisplayBitmap(ID_LinkParamsPassesTypeBlendSpline); \
		break; \
	case UILinkMoveHandlingTypes::UI_ACTION_GAP_BROKEN_FEED_RAP: \
		DisplayBitmap(ID_LinkParamsPassesTypeRetractRapidDistance); \
		break; \
	}

// T E X T   B O X E S

DEFINE_CONTROL_HANDLER( m_txtGapSizeSmallGaps_TextChanged )
{ 
	double value = GetDoubleFromTextBox(m_txtGapSizeSmallGaps); 
	m_scLinkParams->GroupGaps->Size = value;
	DisplayBitmap(ID_LinkParamsGapsSmallSize);
} 
DEFINE_CONTROL_HANDLER( m_txtGapSizeSmallGaps_Enter )
{ 
	DisplayBitmap(ID_LinkParamsGapsSmallSize);
} 
DEFINE_CONTROL_HANDLER( m_txtGapSizeSmallGaps_Leave )
{ 
	EvalAsDouble(m_txtGapSizeSmallGaps);
} 
DEFINE_CONTROL_KEYPRESS_HANDLER( m_txtGapSizeSmallGaps_KeyPress )
{ 
	e->Handled=IsNonNumericKey_Double(e->KeyChar);
} 

DEFINE_CONTROL_HANDLER( m_txtMoveSizeSmallSlices_TextChanged )
{ 
	double value = GetDoubleFromTextBox(m_txtMoveSizeSmallSlices); 
	m_scLinkParams->GroupSlices->Size = value;
	DisplayBitmap(ID_LinkParamsSlicesSmallSize);
} 
DEFINE_CONTROL_HANDLER( m_txtMoveSizeSmallSlices_Enter )
{ 
	DisplayBitmap(ID_LinkParamsSlicesSmallSize);
} 
DEFINE_CONTROL_HANDLER( m_txtMoveSizeSmallSlices_Leave )
{ 
	EvalAsDouble(m_txtMoveSizeSmallSlices);
} 
DEFINE_CONTROL_KEYPRESS_HANDLER( m_txtMoveSizeSmallSlices_KeyPress )
{ 
	e->Handled=IsNonNumericKey_Double(e->KeyChar);
} 

DEFINE_CONTROL_HANDLER( m_txtMoveSizeSmallPasses_TextChanged )
{ 
	double value = GetDoubleFromTextBox(m_txtMoveSizeSmallPasses); 
	m_scLinkParams->GroupPasses->Size = value;
	DisplayBitmap(ID_NONE);
} 
DEFINE_CONTROL_HANDLER( m_txtMoveSizeSmallPasses_Enter )
{ 
	DisplayBitmap(ID_NONE);
} 
DEFINE_CONTROL_HANDLER( m_txtMoveSizeSmallPasses_Leave )
{ 
	EvalAsDouble(m_txtMoveSizeSmallPasses);
} 
DEFINE_CONTROL_KEYPRESS_HANDLER( m_txtMoveSizeSmallPasses_KeyPress )
{ 
	e->Handled=IsNonNumericKey_Double(e->KeyChar);
} 

// C O M B O    B O X E S

#define CALC_LEADS_USE( val, group ) \
	if( m_scLinkParams->Group##group->UseLeadIn##val && m_scLinkParams->Group##group->UseLeadOut##val ) { \
		m_btnLeads##val##group->Enabled = true; \
		DisplayBitmap(ID_LinkParamsUseLeadBoth##group##val); \
	} else if( m_scLinkParams->Group##group->UseLeadIn##val ) { \
		m_btnLeads##val##group->Enabled = true; \
		DisplayBitmap(ID_LinkParamsUseLeadIn##group##val); \
	} else if( m_scLinkParams->Group##group->UseLeadOut##val ) { \
		m_btnLeads##val##group->Enabled = true; \
		DisplayBitmap(ID_LinkParamsUseLeadOut##group##val); \
	} else { \
		m_btnLeads##val##group->Enabled = false; \
		DisplayBitmap(ID_LinkParamsUseLeadNone##group##val); \
	}

#define CALC_ID_LEADS_USE( val, group ) \
	if( m_scLinkParams->Group##group->UseLeadIn##val && m_scLinkParams->Group##group->UseLeadOut##val ) { \
		DisplayBitmap(ID_LinkParamsUseLeadBoth##group##val); \
	} else if( m_scLinkParams->Group##group->UseLeadIn##val ) { \
		DisplayBitmap(ID_LinkParamsUseLeadIn##group##val); \
	} else if( m_scLinkParams->Group##group->UseLeadOut##val ) { \
		DisplayBitmap(ID_LinkParamsUseLeadOut##group##val); \
	} else { \
		DisplayBitmap(ID_LinkParamsUseLeadNone##group##val); \
	}


#define CALC_FIRSTENTRY_IMAGE \
	switch(m_cbLeadInFirstEntry->SelectedIndex){ \
	case LinkFirstEntryTypes::NONE: \
		DisplayBitmap(ID_LinkParamsFirstEntryUseLeadInNot); \
		m_btnLeadsFirstEntry->Enabled = false; \
		break; \
	case LinkFirstEntryTypes::FROM_RAPID_PLANE: \
		DisplayBitmap(ID_LinkParamsFirstEntryClearanceArea); \
		m_btnLeadsFirstEntry->Enabled = true; \
		break; \
	case LinkFirstEntryTypes::USE_RAPID_DISTANCE: \
		DisplayBitmap(ID_LinkParamsFirstEntryRapidDistance); \
		m_btnLeadsFirstEntry->Enabled = true; \
		break; \
	case LinkFirstEntryTypes::USE_FEED_DISTANCE: \
		DisplayBitmap(ID_LinkParamsFirstEntryFeedDistance); \
		m_btnLeadsFirstEntry->Enabled = true; \
		break; \
	}


DEFINE_CONTROL_HANDLER(m_cbLeadInFirstEntry_SelectedIndexChanged)
{
	UILinkFirstEntryTypes uiSelection = static_cast<UILinkFirstEntryTypes>(m_cbLeadInFirstEntry->SelectedIndex);
	if( uiSelection == UILinkFirstEntryTypes::UI_NONE )
	{
		m_scLinkParams->UseLeadInFirstEntry = false;
	}
	else
	{
		m_scLinkParams->UseLeadInFirstEntry = true;
		m_scLinkParams->LinkFirstEntry =  UITypes::UI2DBLinkFirstEntryTypes(uiSelection);
	}
	CALC_FIRSTENTRY_IMAGE
}
DEFINE_CONTROL_HANDLER(m_cbLeadInFirstEntry_Enter)
{
	CALC_FIRSTENTRY_IMAGE
}

#define CALC_LASTEXIT_IMAGE \
	switch(m_cbLeadOutLastExit->SelectedIndex){ \
	case LinkLastExitTypes::NONE: \
		DisplayBitmap(ID_LinkParamsLastExitUseLeadOutNot); \
		m_btnLeadsLastExit->Enabled = false; \
		break; \
	case LinkLastExitTypes::BACK_TO_RAPID_PLANE: \
		DisplayBitmap(ID_LinkParamsLastExitClearanceArea); \
		m_btnLeadsLastExit->Enabled = true; \
		break; \
	case LinkLastExitTypes::USE_RAPID_DISTANCE: \
		DisplayBitmap(ID_LinkParamsLastExitRapidDistance); \
		m_btnLeadsLastExit->Enabled = true; \
		break; \
	case LinkLastExitTypes::USE_FEED_DISTANCE: \
		DisplayBitmap(ID_LinkParamsLastExitFeedDistance); \
		m_btnLeadsLastExit->Enabled = true; \
		break; \
	case LinkLastExitTypes::BACK_TO_CLEARANCE_THROUGH_TUBE_CENTER: \
		DisplayBitmap(ID_LinkParamsLastExitClearanceAreaTubeCenter); \
		m_btnLeadsLastExit->Enabled = true; \
		break; \
	}

DEFINE_CONTROL_HANDLER(m_cbLeadOutLastExit_SelectedIndexChanged)
{
	UILinkLastExitTypes uiSelection = static_cast<UILinkLastExitTypes>(m_cbLeadOutLastExit->SelectedIndex);
	if( uiSelection == UILinkLastExitTypes::UI_NONE )
	{
		m_scLinkParams->UseLeadOutLastExit = false;
	}
	else
	{
		m_scLinkParams->UseLeadOutLastExit = true;
		m_scLinkParams->LinkLastExit =  UITypes::UI2DBLinkLastExitTypes(uiSelection);
	}
	CALC_LASTEXIT_IMAGE
}
DEFINE_CONTROL_HANDLER(m_cbLeadOutLastExit_Enter)
{
	CALC_LASTEXIT_IMAGE
}

DEFINE_COMBOBOX_CONTROL_HANDLER2( SmallGaps, m_scLinkParams->GroupGaps->MoveHandlingSmall, LinkMoveHandling )
	CALC_GAP_MOVE_HANDLING_IMAGE(m_cbSmallGaps)
} 
DEFINE_CONTROL_HANDLER( m_cbSmallGaps_Enter )
{ 
	CALC_GAP_MOVE_HANDLING_IMAGE(m_cbSmallGaps)
} 

DEFINE_COMBOBOX_CONTROL_HANDLER2( LargeGaps, m_scLinkParams->GroupGaps->MoveHandlingLarge, LinkMoveHandling )
	CALC_GAP_MOVE_HANDLING_IMAGE(m_cbLargeGaps)
} 
DEFINE_CONTROL_HANDLER( m_cbLargeGaps_Enter )
{ 
	CALC_GAP_MOVE_HANDLING_IMAGE(m_cbLargeGaps)
} 

DEFINE_COMBOBOX_CONTROL_HANDLER2( SmallSlices, m_scLinkParams->GroupSlices->MoveHandlingSmall, LinkMoveHandling )
	CALC_SLICE_MOVE_HANDLING_IMAGE(m_cbSmallSlices)
} 
DEFINE_CONTROL_HANDLER( m_cbSmallSlices_Enter )
{ 
	CALC_SLICE_MOVE_HANDLING_IMAGE(m_cbSmallSlices)
} 

DEFINE_COMBOBOX_CONTROL_HANDLER2( LargeSlices, m_scLinkParams->GroupSlices->MoveHandlingLarge, LinkMoveHandling )
	CALC_SLICE_MOVE_HANDLING_IMAGE(m_cbLargeSlices)
} 
DEFINE_CONTROL_HANDLER( m_cbLargeSlices_Enter )
{ 
	CALC_SLICE_MOVE_HANDLING_IMAGE(m_cbLargeSlices)
} 

DEFINE_COMBOBOX_CONTROL_HANDLER2( SmallPasses, m_scLinkParams->GroupPasses->MoveHandlingSmall, LinkMoveHandling )
	CALC_PASS_MOVE_HANDLING_IMAGE(m_cbSmallPasses)
} 
DEFINE_CONTROL_HANDLER( m_cbSmallPasses_Enter )
{ 
	CALC_PASS_MOVE_HANDLING_IMAGE(m_cbSmallPasses)
} 

DEFINE_COMBOBOX_CONTROL_HANDLER2( LargePasses, m_scLinkParams->GroupPasses->MoveHandlingLarge, LinkMoveHandling )
	CALC_PASS_MOVE_HANDLING_IMAGE(m_cbLargePasses)
} 
DEFINE_CONTROL_HANDLER( m_cbLargePasses_Enter )
{ 
	CALC_PASS_MOVE_HANDLING_IMAGE(m_cbLargePasses)
} 

DEFINE_CONTROL_HANDLER( m_cbGapSizeSmallGapsType_SelectedIndexChanged )
{
	m_scLinkParams->GroupGaps->SetLeadsParamsSizeType((UITypes::UI2DBLeadsParamsSizeTypes(static_cast<UILeadsParamsSizeTypes>(m_cbGapSizeSmallGapsType->SelectedIndex))));
	DisplayBitmap(ID_LinkParamsGapsSmallSize);
} 
DEFINE_CONTROL_HANDLER( m_cbGapSizeSmallGapsType_Enter )
{ 
	DisplayBitmap(ID_LinkParamsGapsSmallSize);
} 

DEFINE_CONTROL_HANDLER( m_cbMoveSizeSmallSlicesType_SelectedIndexChanged )
{
	m_scLinkParams->GroupSlices->SetLeadsParamsSizeType((UITypes::UI2DBLeadsParamsSizeTypes(static_cast<UILeadsParamsSizeTypes>(m_cbMoveSizeSmallSlicesType->SelectedIndex))));
	DisplayBitmap(ID_LinkParamsSlicesSmallSize);
} 
DEFINE_CONTROL_HANDLER( m_cbMoveSizeSmallSlicesType_Enter )
{ 
	DisplayBitmap(ID_LinkParamsSlicesSmallSize);
} 
/*
DEFINE_CONTROL_HANDLER( m_cbMoveSizeSmallPassesType_SelectedIndexChanged )
{
	m_scLinkParams->GroupPasses->SetLeadsParamsSizeType((UITypes::UI2DBLeadsParamsSizeTypes(static_cast<UILeadsParamsSizeTypes>(m_cbMoveSizeSmallPassesType->SelectedIndex))));
	DisplayBitmap(ID_PassesSmallSize);
} 
DEFINE_CONTROL_HANDLER( m_cbMoveSizeSmallPassesType_Enter )
{ 
	DisplayBitmap(ID_PassesSmallSize);
} */

DEFINE_CONTROL_HANDLER( m_cbSmallGapsUse_SelectedIndexChanged )
{
	m_scLinkParams->GroupGaps->SetLeadParamsUse((UITypes::UI2DBLeadsParamsUseTypes(static_cast<UILeadsParamsUseTypes>(m_cbSmallGapsUse->SelectedIndex))), true);
	CALC_LEADS_USE( Small, Gaps )
} 
DEFINE_CONTROL_HANDLER( m_cbSmallGapsUse_Enter )
{ 
	CALC_ID_LEADS_USE( Small, Gaps )
} 

DEFINE_CONTROL_HANDLER( m_cbLargeGapsUse_SelectedIndexChanged )
{
	m_scLinkParams->GroupGaps->SetLeadParamsUse((UITypes::UI2DBLeadsParamsUseTypes(static_cast<UILeadsParamsUseTypes>(m_cbLargeGapsUse->SelectedIndex))), false);
	CALC_LEADS_USE( Large, Gaps )
} 
DEFINE_CONTROL_HANDLER( m_cbLargeGapsUse_Enter )
{ 
	CALC_ID_LEADS_USE( Large, Gaps )
} 

DEFINE_CONTROL_HANDLER( m_cbSmallSlicesUse_SelectedIndexChanged )
{
	if( m_cbSmallSlicesUse->Focused )
	{
		m_scLinkParams->GroupSlices->SetLeadParamsUse((UITypes::UI2DBLeadsParamsUseTypes(static_cast<UILeadsParamsUseTypes>(m_cbSmallSlicesUse->SelectedIndex))), true);
		/*if( m_scLinkParams->GroupSlices->UseLeadInSmall )
			m_LeadLinkZigControlCheckStatesFlags |= 1;
		else
			m_LeadLinkZigControlCheckStatesFlags &= 14;
		if( m_scLinkParams->GroupSlices->UseLeadOutSmall )
			m_LeadLinkZigControlCheckStatesFlags |= 2;
		else
			m_LeadLinkZigControlCheckStatesFlags &= 13;*/
	}
	CALC_LEADS_USE( Small, Slices )
} 
DEFINE_CONTROL_HANDLER( m_cbSmallSlicesUse_Enter )
{ 
	CALC_ID_LEADS_USE( Small, Slices )
} 

DEFINE_CONTROL_HANDLER( m_cbLargeSlicesUse_SelectedIndexChanged )
{
	if( m_cbLargeSlicesUse->Focused )
	{
		m_scLinkParams->GroupSlices->SetLeadParamsUse((UITypes::UI2DBLeadsParamsUseTypes(static_cast<UILeadsParamsUseTypes>(m_cbLargeSlicesUse->SelectedIndex))), false);
		/*if( m_scLinkParams->GroupSlices->UseLeadInLarge )
			m_LeadLinkZigControlCheckStatesFlags |= 4;
		else
			m_LeadLinkZigControlCheckStatesFlags &= 11;
		if( m_scLinkParams->GroupSlices->UseLeadOutLarge )
			m_LeadLinkZigControlCheckStatesFlags |= 8;
		else
			m_LeadLinkZigControlCheckStatesFlags &= 7;*/
	}
	CALC_LEADS_USE( Large, Slices )
} 
DEFINE_CONTROL_HANDLER( m_cbLargeSlicesUse_Enter )
{ 
	CALC_ID_LEADS_USE( Large, Slices )
} 

DEFINE_CONTROL_HANDLER( m_cbSmallPassesUse_SelectedIndexChanged )
{
	if( m_cbSmallPassesUse->Focused )
	{
		m_scLinkParams->GroupPasses->SetLeadParamsUse((UITypes::UI2DBLeadsParamsUseTypes(static_cast<UILeadsParamsUseTypes>(m_cbSmallPassesUse->SelectedIndex))), true);
		/*if( m_scLinkParams->GroupPasses->UseLeadInSmall )
			m_LeadLinkZigControlCheckStatesFlags |= 1;
		else
			m_LeadLinkZigControlCheckStatesFlags &= 14;
		if( m_scLinkParams->GroupPasses->UseLeadOutSmall )
			m_LeadLinkZigControlCheckStatesFlags |= 2;
		else
			m_LeadLinkZigControlCheckStatesFlags &= 13;*/
	}
	CALC_LEADS_USE( Small, Passes )
} 
DEFINE_CONTROL_HANDLER( m_cbSmallPassesUse_Enter )
{ 
	CALC_ID_LEADS_USE( Small, Passes )
} 

DEFINE_CONTROL_HANDLER( m_cbLargePassesUse_SelectedIndexChanged )
{
	if( m_cbLargePassesUse->Focused )
	{
		m_scLinkParams->GroupPasses->SetLeadParamsUse((UITypes::UI2DBLeadsParamsUseTypes(static_cast<UILeadsParamsUseTypes>(m_cbLargePassesUse->SelectedIndex))), false);
		/*if( m_scLinkParams->GroupPasses->UseLeadInLarge )
			m_LeadLinkZigControlCheckStatesFlags |= 4;
		else
			m_LeadLinkZigControlCheckStatesFlags &= 11;
		if( m_scLinkParams->GroupPasses->UseLeadOutLarge )
			m_LeadLinkZigControlCheckStatesFlags |= 8;
		else
			m_LeadLinkZigControlCheckStatesFlags &= 7;*/
	}
	CALC_LEADS_USE( Large, Passes )
} 
DEFINE_CONTROL_HANDLER( m_cbLargePassesUse_Enter )
{ 
	CALC_ID_LEADS_USE( Large, Passes )
} 

// B U T T O N S 

DEFINE_CONTROL_HANDLER( m_btnLeadsFirstEntry_Click )
{ 
	LinkParamsLeadsDlg ^ m_LinkParamsDlg2 = gcnew LinkParamsLeadsDlg(m_ResourceManager,this);
	//LinkParamsLeadsDlg ^ m_LinkParamsDlg2 = dynamic_cast<LinkParamsLeadsDlg^>(m_LinkParamsDlg);
	if( m_scLinkParams->UseDefaultLeadInFirstEntry )
		m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->LeadInDefault);
	else
		m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->LeadInFirstEntry);

	m_LinkParamsDlg2->SetMode(m_LeadInOutTitleLeadIn,MODE_LEADIN,m_scLinkParams->UseDefaultLeadInFirstEntry,false);
	
	System::Windows::Forms::DialogResult dlgRes = m_LinkParamsDlg2->ShowDialog(this);
	if (dlgRes == System::Windows::Forms::DialogResult::OK)
	{
		m_scLinkParams->UseDefaultLeadInFirstEntry = m_LinkParamsDlg2->GetUseDefaults();
		if( *m_scLinkParams->LeadInDefault == *m_LinkParamsDlg2->m_scParamsLeadIn )
			m_scLinkParams->UseDefaultLeadInFirstEntry = true;
		if( m_scLinkParams->UseDefaultLeadInFirstEntry )
			*m_scLinkParams->LeadInFirstEntry = *m_scLinkParams->LeadInDefault;
		else
			*m_scLinkParams->LeadInFirstEntry = *m_LinkParamsDlg2->m_scParamsLeadIn;
	}

	/*UIRetraceTypes retraceType = static_cast<UIRetraceTypes>(m_cbRetraceType->SelectedIndex);
	if (retraceType == UIRetraceTypes::UI_RETRACE_TYPE_ZIG)
	{
		if( m_scLinkParams->UseLeadInFirstEntry && m_scLinkParams->UseLeadOutLastExit )
		{
			m_cbSmallSlicesUse->SelectedIndex = 2;
			m_cbLargeSlicesUse->SelectedIndex = 2;
		}
		else if( m_scLinkParams->UseLeadInFirstEntry )
		{
			m_cbSmallSlicesUse->SelectedIndex = 0;
			m_cbLargeSlicesUse->SelectedIndex = 0;
		}
		else if( m_scLinkParams->UseLeadOutLastExit )
		{
			m_cbSmallSlicesUse->SelectedIndex = 1;
			m_cbLargeSlicesUse->SelectedIndex = 1;
		}
		else
		{
			m_cbSmallSlicesUse->SelectedIndex = 3;
			m_cbLargeSlicesUse->SelectedIndex = 3;
		}
		if( m_chkSpiralFlag->Checked )
		{
			m_cbSmallSlicesUse->SelectedIndex = 3;
		}
	}
	else
	{
		if( m_LeadLinkZigControlCheckStatesFlags & 1 && m_LeadLinkZigControlCheckStatesFlags & 2 )
			m_cbSmallSlicesUse->SelectedIndex = 2;
		else if( m_LeadLinkZigControlCheckStatesFlags & 1 )
			m_cbSmallSlicesUse->SelectedIndex = 0;
		else if( m_LeadLinkZigControlCheckStatesFlags & 2 )
			m_cbSmallSlicesUse->SelectedIndex = 1;
		else
			m_cbSmallSlicesUse->SelectedIndex = 3;

		if( m_LeadLinkZigControlCheckStatesFlags & 4 && m_LeadLinkZigControlCheckStatesFlags & 8 )
			m_cbLargeSlicesUse->SelectedIndex = 2;
		else if( m_LeadLinkZigControlCheckStatesFlags & 4 )
			m_cbLargeSlicesUse->SelectedIndex = 0;
		else if( m_LeadLinkZigControlCheckStatesFlags & 8 )
			m_cbLargeSlicesUse->SelectedIndex = 1;
		else
			m_cbLargeSlicesUse->SelectedIndex = 3;
	}*/
	CALC_FIRST_ENTRY_IMAGE
} 

DEFINE_CONTROL_HANDLER( m_btnLeadsLastExit_Click )
{ 
	LinkParamsLeadsDlg ^ m_LinkParamsDlg2 = gcnew LinkParamsLeadsDlg(m_ResourceManager,this);
	if( m_scLinkParams->UseDefaultLeadOutLastExit )
		m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->LeadOutDefault);
	else
		m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->LeadOutLastExit);

	m_LinkParamsDlg2->SetMode(m_LeadInOutTitleLeadOut,MODE_LEADOUT,m_scLinkParams->UseDefaultLeadOutLastExit,false);
	
	System::Windows::Forms::DialogResult dlgRes = m_LinkParamsDlg2->ShowDialog(this);
	if (dlgRes == System::Windows::Forms::DialogResult::OK)
	{
		m_scLinkParams->UseDefaultLeadOutLastExit = m_LinkParamsDlg2->GetUseDefaults();
		if( *m_scLinkParams->LeadOutDefault == *m_LinkParamsDlg2->m_scParamsLeadOut )
			m_scLinkParams->UseDefaultLeadOutLastExit = true;
		if( m_scLinkParams->UseDefaultLeadOutLastExit )
			*m_scLinkParams->LeadOutLastExit = *m_scLinkParams->LeadOutDefault;
		else
			*m_scLinkParams->LeadOutLastExit = *m_LinkParamsDlg2->m_scParamsLeadOut;
	}

	/*UIRetraceTypes retraceType = static_cast<UIRetraceTypes>(m_cbRetraceType->SelectedIndex);
	if (retraceType == UIRetraceTypes::UI_RETRACE_TYPE_ZIG)
	{
		if( m_scLinkParams->UseLeadInFirstEntry && m_scLinkParams->UseLeadOutLastExit )
		{
			m_cbSmallSlicesUse->SelectedIndex = 2;
			m_cbLargeSlicesUse->SelectedIndex = 2;
		}
		else if( m_scLinkParams->UseLeadInFirstEntry )
		{
			m_cbSmallSlicesUse->SelectedIndex = 0;
			m_cbLargeSlicesUse->SelectedIndex = 0;
		}
		else if( m_scLinkParams->UseLeadOutLastExit )
		{
			m_cbSmallSlicesUse->SelectedIndex = 1;
			m_cbLargeSlicesUse->SelectedIndex = 1;
		}
		else
		{
			m_cbSmallSlicesUse->SelectedIndex = 3;
			m_cbLargeSlicesUse->SelectedIndex = 3;
		}
		if( m_chkSpiralFlag->Checked )
		{
			m_cbSmallSlicesUse->SelectedIndex = 3;
		}
	}
	else
	{
		if( m_LeadLinkZigControlCheckStatesFlags & 1 && m_LeadLinkZigControlCheckStatesFlags & 2 )
			m_cbSmallSlicesUse->SelectedIndex = 2;
		else if( m_LeadLinkZigControlCheckStatesFlags & 1 )
			m_cbSmallSlicesUse->SelectedIndex = 0;
		else if( m_LeadLinkZigControlCheckStatesFlags & 2 )
			m_cbSmallSlicesUse->SelectedIndex = 1;
		else
			m_cbSmallSlicesUse->SelectedIndex = 3;

		if( m_LeadLinkZigControlCheckStatesFlags & 4 && m_LeadLinkZigControlCheckStatesFlags & 8 )
			m_cbLargeSlicesUse->SelectedIndex = 2;
		else if( m_LeadLinkZigControlCheckStatesFlags & 4 )
			m_cbLargeSlicesUse->SelectedIndex = 0;
		else if( m_LeadLinkZigControlCheckStatesFlags & 8 )
			m_cbLargeSlicesUse->SelectedIndex = 1;
		else
			m_cbLargeSlicesUse->SelectedIndex = 3;
	}*/
	CALC_LAST_EXIT_IMAGE
} 

DEFINE_CONTROL_HANDLER( m_btnLeadsSmallGaps_Click )
{ 
	LinkParamsLeadsDlg ^ m_LinkParamsDlg2 = gcnew LinkParamsLeadsDlg(m_ResourceManager,this);
	switch( m_cbSmallGapsUse->SelectedIndex )
	{
	case 0:
		if( *m_scLinkParams->LeadInDefault == *m_scLinkParams->GroupGaps->LeadInSmall )
			m_scLinkParams->GroupGaps->UseDefaultSmall = true;
		if( m_scLinkParams->GroupGaps->UseDefaultSmall )
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->LeadInDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->GroupGaps->LeadInSmall);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleSmallGaps,MODE_LEADIN,m_scLinkParams->GroupGaps->UseDefaultSmall,false);
		break;
	case 1:
		if( *m_scLinkParams->LeadOutDefault == *m_scLinkParams->GroupGaps->LeadOutSmall )
			m_scLinkParams->GroupGaps->UseDefaultSmall = true;
		if( m_scLinkParams->GroupGaps->UseDefaultSmall )
		{
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->LeadOutDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->GroupGaps->LeadOutSmall);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleSmallGaps,MODE_LEADOUT,m_scLinkParams->GroupGaps->UseDefaultSmall,false);
		break;
	case 2:
		if( *m_scLinkParams->LeadInDefault == *m_scLinkParams->GroupGaps->LeadInSmall &&
			*m_scLinkParams->LeadOutDefault == *m_scLinkParams->GroupGaps->LeadOutSmall )
			m_scLinkParams->GroupGaps->UseDefaultSmall = true;
		if( m_scLinkParams->GroupGaps->UseDefaultSmall )
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->LeadInDefault);
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->LeadOutDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->GroupGaps->LeadInSmall);
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->GroupGaps->LeadOutSmall);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleSmallGaps,MODE_BOTH,m_scLinkParams->GroupGaps->UseDefaultSmall,false);
		break;
	}
	System::Windows::Forms::DialogResult dlgRes = m_LinkParamsDlg2->ShowDialog(this);
	if (dlgRes == System::Windows::Forms::DialogResult::OK)
	{
		m_scLinkParams->GroupGaps->UseDefaultSmall = m_LinkParamsDlg2->GetUseDefaults();
		switch( m_cbSmallGapsUse->SelectedIndex )
		{
		case 0:
			if( *m_scLinkParams->LeadInDefault == *m_LinkParamsDlg2->m_scParamsLeadIn )
				m_scLinkParams->GroupGaps->UseDefaultSmall = true;
			if( m_scLinkParams->GroupGaps->UseDefaultSmall )
			{
				*m_scLinkParams->GroupGaps->LeadInSmall = *m_scLinkParams->LeadInDefault;
			}
			else
			{
				*m_scLinkParams->GroupGaps->LeadInSmall = *m_LinkParamsDlg2->m_scParamsLeadIn;
			}
			break;
		case 1:
			if( *m_scLinkParams->LeadOutDefault == *m_LinkParamsDlg2->m_scParamsLeadOut )
				m_scLinkParams->GroupGaps->UseDefaultSmall = true;
			if( m_scLinkParams->GroupGaps->UseDefaultSmall )
			{
				*m_scLinkParams->GroupGaps->LeadOutSmall = *m_scLinkParams->LeadOutDefault;
			}
			else
			{
				*m_scLinkParams->GroupGaps->LeadOutSmall = *m_LinkParamsDlg2->m_scParamsLeadOut;
			}
			break;
		case 2:
			if( *m_scLinkParams->LeadInDefault == *m_LinkParamsDlg2->m_scParamsLeadIn &&
				*m_scLinkParams->LeadOutDefault == *m_LinkParamsDlg2->m_scParamsLeadOut )
				m_scLinkParams->GroupGaps->UseDefaultSmall = true;
			if( m_scLinkParams->GroupGaps->UseDefaultSmall )
			{
				*m_scLinkParams->GroupGaps->LeadInSmall = *m_scLinkParams->LeadInDefault;
				*m_scLinkParams->GroupGaps->LeadOutSmall = *m_scLinkParams->LeadOutDefault;
			}
			else
			{
				*m_scLinkParams->GroupGaps->LeadInSmall = *m_LinkParamsDlg2->m_scParamsLeadIn;
				*m_scLinkParams->GroupGaps->LeadOutSmall = *m_LinkParamsDlg2->m_scParamsLeadOut;
			}
			break;
		}
	}
} 

DEFINE_CONTROL_HANDLER( m_btnLeadsLargeGaps_Click )
{ 
	LinkParamsLeadsDlg ^ m_LinkParamsDlg2 = gcnew LinkParamsLeadsDlg(m_ResourceManager,this);
	switch( m_cbLargeGapsUse->SelectedIndex )
	{
	case 0:
		if( *m_scLinkParams->LeadInDefault == *m_scLinkParams->GroupGaps->LeadInLarge )
			m_scLinkParams->GroupGaps->UseDefaultLarge = true;
		if( m_scLinkParams->GroupGaps->UseDefaultLarge )
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->LeadInDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->GroupGaps->LeadInLarge);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleLargeGaps,MODE_LEADIN,m_scLinkParams->GroupGaps->UseDefaultLarge,false);
		break;
	case 1:
		if( *m_scLinkParams->LeadOutDefault == *m_scLinkParams->GroupGaps->LeadOutLarge )
			m_scLinkParams->GroupGaps->UseDefaultLarge = true;
		if( m_scLinkParams->GroupGaps->UseDefaultLarge )
		{
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->LeadOutDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->GroupGaps->LeadOutLarge);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleLargeGaps,MODE_LEADOUT,m_scLinkParams->GroupGaps->UseDefaultLarge,false);
		break;
	case 2:
		if( *m_scLinkParams->LeadInDefault == *m_scLinkParams->GroupGaps->LeadInLarge &&
			*m_scLinkParams->LeadOutDefault == *m_scLinkParams->GroupGaps->LeadOutLarge )
			m_scLinkParams->GroupGaps->UseDefaultLarge = true;
		if( m_scLinkParams->GroupGaps->UseDefaultLarge )
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->LeadInDefault);
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->LeadOutDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->GroupGaps->LeadInLarge);
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->GroupGaps->LeadOutLarge);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleLargeGaps,MODE_BOTH,m_scLinkParams->GroupGaps->UseDefaultLarge,false);
		break;
	}
	System::Windows::Forms::DialogResult dlgRes = m_LinkParamsDlg2->ShowDialog(this);
	if (dlgRes == System::Windows::Forms::DialogResult::OK)
	{
		m_scLinkParams->GroupGaps->UseDefaultLarge = m_LinkParamsDlg2->GetUseDefaults();
		switch( m_cbLargeGapsUse->SelectedIndex )
		{
		case 0:
			if( *m_scLinkParams->LeadInDefault == *m_LinkParamsDlg2->m_scParamsLeadIn )
				m_scLinkParams->GroupGaps->UseDefaultLarge = true;
			if( m_scLinkParams->GroupGaps->UseDefaultLarge )
			{
				*m_scLinkParams->GroupGaps->LeadInLarge = *m_scLinkParams->LeadInDefault;
			}
			else
			{
				*m_scLinkParams->GroupGaps->LeadInLarge = *m_LinkParamsDlg2->m_scParamsLeadIn;
			}
			break;
		case 1:
			if( *m_scLinkParams->LeadOutDefault == *m_LinkParamsDlg2->m_scParamsLeadOut )
				m_scLinkParams->GroupGaps->UseDefaultLarge = true;
			if( m_scLinkParams->GroupGaps->UseDefaultLarge )
			{
				*m_scLinkParams->GroupGaps->LeadOutLarge = *m_scLinkParams->LeadOutDefault;
			}
			else
			{
				*m_scLinkParams->GroupGaps->LeadOutLarge = *m_LinkParamsDlg2->m_scParamsLeadOut;
			}
			break;
		case 2:
			if( *m_scLinkParams->LeadInDefault == *m_LinkParamsDlg2->m_scParamsLeadIn &&
				*m_scLinkParams->LeadOutDefault == *m_LinkParamsDlg2->m_scParamsLeadOut )
				m_scLinkParams->GroupGaps->UseDefaultLarge = true;
			if( m_scLinkParams->GroupGaps->UseDefaultLarge )
			{
				*m_scLinkParams->GroupGaps->LeadInLarge = *m_scLinkParams->LeadInDefault;
				*m_scLinkParams->GroupGaps->LeadOutLarge = *m_scLinkParams->LeadOutDefault;
			}
			else
			{
				*m_scLinkParams->GroupGaps->LeadInLarge = *m_LinkParamsDlg2->m_scParamsLeadIn;
				*m_scLinkParams->GroupGaps->LeadOutLarge = *m_LinkParamsDlg2->m_scParamsLeadOut;
			}
			break;
		}
	}
} 

DEFINE_CONTROL_HANDLER( m_btnLeadsSmallSlices_Click )
{ 
	LinkParamsLeadsDlg ^ m_LinkParamsDlg2 = gcnew LinkParamsLeadsDlg(m_ResourceManager,this);
	switch( m_cbSmallSlicesUse->SelectedIndex )
	{
	case 0:
		if( *m_scLinkParams->LeadInDefault == *m_scLinkParams->GroupSlices->LeadInSmall )
			m_scLinkParams->GroupSlices->UseDefaultSmall = true;
		if( m_scLinkParams->GroupSlices->UseDefaultSmall )
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->LeadInDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->GroupSlices->LeadInSmall);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleSmallLinks,MODE_LEADIN,m_scLinkParams->GroupSlices->UseDefaultSmall,false);
		break;
	case 1:
		if( *m_scLinkParams->LeadOutDefault == *m_scLinkParams->GroupSlices->LeadOutSmall )
			m_scLinkParams->GroupSlices->UseDefaultSmall = true;
		if( m_scLinkParams->GroupSlices->UseDefaultSmall )
		{
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->LeadOutDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->GroupSlices->LeadOutSmall);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleSmallLinks,MODE_LEADOUT,m_scLinkParams->GroupSlices->UseDefaultSmall,false);
		break;
	case 2:
		if( *m_scLinkParams->LeadInDefault == *m_scLinkParams->GroupSlices->LeadInSmall &&
			*m_scLinkParams->LeadOutDefault == *m_scLinkParams->GroupSlices->LeadOutSmall )
			m_scLinkParams->GroupSlices->UseDefaultSmall = true;
		if( m_scLinkParams->GroupSlices->UseDefaultSmall )
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->LeadInDefault);
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->LeadOutDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->GroupSlices->LeadInSmall);
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->GroupSlices->LeadOutSmall);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleSmallLinks,MODE_BOTH,m_scLinkParams->GroupSlices->UseDefaultSmall,false);
		break;
	}
	System::Windows::Forms::DialogResult dlgRes = m_LinkParamsDlg2->ShowDialog(this);
	if (dlgRes == System::Windows::Forms::DialogResult::OK)
	{
		m_scLinkParams->GroupSlices->UseDefaultSmall = m_LinkParamsDlg2->GetUseDefaults();
		switch( m_cbSmallSlicesUse->SelectedIndex )
		{
		case 0:
			if( *m_scLinkParams->LeadInDefault == *m_LinkParamsDlg2->m_scParamsLeadIn )
				m_scLinkParams->GroupSlices->UseDefaultSmall = true;
			if( m_scLinkParams->GroupSlices->UseDefaultSmall )
			{
				*m_scLinkParams->GroupSlices->LeadInSmall = *m_scLinkParams->LeadInDefault;
			}
			else
			{
				*m_scLinkParams->GroupSlices->LeadInSmall = *m_LinkParamsDlg2->m_scParamsLeadIn;
			}
			break;
		case 1:
			if( *m_scLinkParams->LeadOutDefault == *m_LinkParamsDlg2->m_scParamsLeadOut )
				m_scLinkParams->GroupSlices->UseDefaultSmall = true;
			if( m_scLinkParams->GroupSlices->UseDefaultSmall )
			{
				*m_scLinkParams->GroupSlices->LeadOutSmall = *m_scLinkParams->LeadOutDefault;
			}
			else
			{
				*m_scLinkParams->GroupSlices->LeadOutSmall = *m_LinkParamsDlg2->m_scParamsLeadOut;
			}
			break;
		case 2:
			if( *m_scLinkParams->LeadInDefault == *m_LinkParamsDlg2->m_scParamsLeadIn &&
				*m_scLinkParams->LeadOutDefault == *m_LinkParamsDlg2->m_scParamsLeadOut )
				m_scLinkParams->GroupSlices->UseDefaultSmall = true;
			if( m_scLinkParams->GroupSlices->UseDefaultSmall )
			{
				*m_scLinkParams->GroupSlices->LeadInSmall = *m_scLinkParams->LeadInDefault;
				*m_scLinkParams->GroupSlices->LeadOutSmall = *m_scLinkParams->LeadOutDefault;
			}
			else
			{
				*m_scLinkParams->GroupSlices->LeadInSmall = *m_LinkParamsDlg2->m_scParamsLeadIn;
				*m_scLinkParams->GroupSlices->LeadOutSmall = *m_LinkParamsDlg2->m_scParamsLeadOut;
			}
			break;
		}
	}
} 

DEFINE_CONTROL_HANDLER( m_btnLeadsLargeSlices_Click )
{ 
	LinkParamsLeadsDlg ^ m_LinkParamsDlg2 = gcnew LinkParamsLeadsDlg(m_ResourceManager,this);
	switch( m_cbLargeSlicesUse->SelectedIndex )
	{
	case 0:
		if( *m_scLinkParams->LeadInDefault == *m_scLinkParams->GroupSlices->LeadInLarge )
			m_scLinkParams->GroupSlices->UseDefaultLarge = true;
		if( m_scLinkParams->GroupSlices->UseDefaultLarge )
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->LeadInDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->GroupSlices->LeadInLarge);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleLargeLinks,MODE_LEADIN,m_scLinkParams->GroupSlices->UseDefaultLarge,false);
		break;
	case 1:
		if( *m_scLinkParams->LeadOutDefault == *m_scLinkParams->GroupSlices->LeadOutLarge )
			m_scLinkParams->GroupSlices->UseDefaultLarge = true;
		if( m_scLinkParams->GroupSlices->UseDefaultLarge )
		{
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->LeadOutDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->GroupSlices->LeadOutLarge);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleLargeLinks,MODE_LEADOUT,m_scLinkParams->GroupSlices->UseDefaultLarge,false);
		break;
	case 2:
		if( *m_scLinkParams->LeadInDefault == *m_scLinkParams->GroupSlices->LeadInLarge &&
			*m_scLinkParams->LeadOutDefault == *m_scLinkParams->GroupSlices->LeadOutLarge )
			m_scLinkParams->GroupSlices->UseDefaultLarge = true;
		if( m_scLinkParams->GroupSlices->UseDefaultLarge )
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->LeadInDefault);
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->LeadOutDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->GroupSlices->LeadInLarge);
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->GroupSlices->LeadOutLarge);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleLargeLinks,MODE_BOTH,m_scLinkParams->GroupSlices->UseDefaultLarge,false);
		break;
	}
	System::Windows::Forms::DialogResult dlgRes = m_LinkParamsDlg2->ShowDialog(this);
	if (dlgRes == System::Windows::Forms::DialogResult::OK)
	{
		m_scLinkParams->GroupSlices->UseDefaultLarge = m_LinkParamsDlg2->GetUseDefaults();
		switch( m_cbLargeSlicesUse->SelectedIndex )
		{
		case 0:
			if( *m_scLinkParams->LeadInDefault == *m_LinkParamsDlg2->m_scParamsLeadIn )
				m_scLinkParams->GroupSlices->UseDefaultLarge = true;
			if( m_scLinkParams->GroupSlices->UseDefaultLarge )
			{
				*m_scLinkParams->GroupSlices->LeadInLarge = *m_scLinkParams->LeadInDefault;
			}
			else
			{
				*m_scLinkParams->GroupSlices->LeadInLarge = *m_LinkParamsDlg2->m_scParamsLeadIn;
			}
			break;
		case 1:
			if( *m_scLinkParams->LeadOutDefault == *m_LinkParamsDlg2->m_scParamsLeadOut )
				m_scLinkParams->GroupSlices->UseDefaultLarge = true;
			if( m_scLinkParams->GroupSlices->UseDefaultLarge )
			{
				*m_scLinkParams->GroupSlices->LeadOutLarge = *m_scLinkParams->LeadOutDefault;
			}
			else
			{
				*m_scLinkParams->GroupSlices->LeadOutLarge = *m_LinkParamsDlg2->m_scParamsLeadOut;
			}
			break;
		case 2:
			if( *m_scLinkParams->LeadInDefault == *m_LinkParamsDlg2->m_scParamsLeadIn &&
				*m_scLinkParams->LeadOutDefault == *m_LinkParamsDlg2->m_scParamsLeadOut )
				m_scLinkParams->GroupSlices->UseDefaultLarge = true;
			if( m_scLinkParams->GroupSlices->UseDefaultLarge )
			{
				*m_scLinkParams->GroupSlices->LeadInLarge = *m_scLinkParams->LeadInDefault;
				*m_scLinkParams->GroupSlices->LeadOutLarge = *m_scLinkParams->LeadOutDefault;
			}
			else
			{
				*m_scLinkParams->GroupSlices->LeadInLarge = *m_LinkParamsDlg2->m_scParamsLeadIn;
				*m_scLinkParams->GroupSlices->LeadOutLarge = *m_LinkParamsDlg2->m_scParamsLeadOut;
			}
			break;
		}
	}
} 

DEFINE_CONTROL_HANDLER( m_btnLeadsSmallPasses_Click )
{ 
	LinkParamsLeadsDlg ^ m_LinkParamsDlg2 = gcnew LinkParamsLeadsDlg(m_ResourceManager,this);
	switch( m_cbSmallPassesUse->SelectedIndex )
	{
	case 0:
		if( *m_scLinkParams->LeadInDefault == *m_scLinkParams->GroupPasses->LeadInSmall )
			m_scLinkParams->GroupPasses->UseDefaultSmall = true;
		if( m_scLinkParams->GroupPasses->UseDefaultSmall )
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->LeadInDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->GroupPasses->LeadInSmall);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleSmallPasses,MODE_LEADIN,m_scLinkParams->GroupPasses->UseDefaultSmall,false);
		break;
	case 1:
		if( *m_scLinkParams->LeadOutDefault == *m_scLinkParams->GroupPasses->LeadOutSmall )
			m_scLinkParams->GroupPasses->UseDefaultSmall = true;
		if( m_scLinkParams->GroupPasses->UseDefaultSmall )
		{
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->LeadOutDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->GroupPasses->LeadOutSmall);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleSmallPasses,MODE_LEADOUT,m_scLinkParams->GroupPasses->UseDefaultSmall,false);
		break;
	case 2:
		if( *m_scLinkParams->LeadInDefault == *m_scLinkParams->GroupPasses->LeadInSmall &&
			*m_scLinkParams->LeadOutDefault == *m_scLinkParams->GroupPasses->LeadOutSmall )
			m_scLinkParams->GroupPasses->UseDefaultSmall = true;
		if( m_scLinkParams->GroupPasses->UseDefaultSmall )
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->LeadInDefault);
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->LeadOutDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->GroupPasses->LeadInSmall);
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->GroupPasses->LeadOutSmall);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleSmallPasses,MODE_BOTH,m_scLinkParams->GroupPasses->UseDefaultSmall,false);
		break;
	}
	System::Windows::Forms::DialogResult dlgRes = m_LinkParamsDlg2->ShowDialog(this);
	if (dlgRes == System::Windows::Forms::DialogResult::OK)
	{
		m_scLinkParams->GroupPasses->UseDefaultSmall = m_LinkParamsDlg2->GetUseDefaults();
		switch( m_cbSmallPassesUse->SelectedIndex )
		{
		case 0:
			if( *m_scLinkParams->LeadInDefault == *m_LinkParamsDlg2->m_scParamsLeadIn )
				m_scLinkParams->GroupPasses->UseDefaultSmall = true;
			if( m_scLinkParams->GroupPasses->UseDefaultSmall )
			{
				*m_scLinkParams->GroupPasses->LeadInSmall = *m_scLinkParams->LeadInDefault;
			}
			else
			{
				*m_scLinkParams->GroupPasses->LeadInSmall = *m_LinkParamsDlg2->m_scParamsLeadIn;
			}
			break;
		case 1:
			if( *m_scLinkParams->LeadOutDefault == *m_LinkParamsDlg2->m_scParamsLeadOut )
				m_scLinkParams->GroupPasses->UseDefaultSmall = true;
			if( m_scLinkParams->GroupPasses->UseDefaultSmall )
			{
				*m_scLinkParams->GroupPasses->LeadOutSmall = *m_scLinkParams->LeadOutDefault;
			}
			else
			{
				*m_scLinkParams->GroupPasses->LeadOutSmall = *m_LinkParamsDlg2->m_scParamsLeadOut;
			}
			break;
		case 2:
			if( *m_scLinkParams->LeadInDefault == *m_LinkParamsDlg2->m_scParamsLeadIn &&
				*m_scLinkParams->LeadOutDefault == *m_LinkParamsDlg2->m_scParamsLeadOut )
				m_scLinkParams->GroupPasses->UseDefaultSmall = true;
			if( m_scLinkParams->GroupPasses->UseDefaultSmall )
			{
				*m_scLinkParams->GroupPasses->LeadInSmall = *m_scLinkParams->LeadInDefault;
				*m_scLinkParams->GroupPasses->LeadOutSmall = *m_scLinkParams->LeadOutDefault;
			}
			else
			{
				*m_scLinkParams->GroupPasses->LeadInSmall = *m_LinkParamsDlg2->m_scParamsLeadIn;
				*m_scLinkParams->GroupPasses->LeadOutSmall = *m_LinkParamsDlg2->m_scParamsLeadOut;
			}
			break;
		}
	}
} 

DEFINE_CONTROL_HANDLER( m_btnLeadsLargePasses_Click )
{ 
	LinkParamsLeadsDlg ^ m_LinkParamsDlg2 = gcnew LinkParamsLeadsDlg(m_ResourceManager,this);
	switch( m_cbLargePassesUse->SelectedIndex )
	{
	case 0:
		if( *m_scLinkParams->LeadInDefault == *m_scLinkParams->GroupPasses->LeadInLarge )
			m_scLinkParams->GroupPasses->UseDefaultLarge = true;
		if( m_scLinkParams->GroupPasses->UseDefaultLarge )
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->LeadInDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->GroupPasses->LeadInLarge);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleLargePasses,MODE_LEADIN,m_scLinkParams->GroupPasses->UseDefaultLarge,false);
		break;
	case 1:
		if( *m_scLinkParams->LeadOutDefault == *m_scLinkParams->GroupPasses->LeadOutLarge )
			m_scLinkParams->GroupPasses->UseDefaultLarge = true;
		if( m_scLinkParams->GroupPasses->UseDefaultLarge )
		{
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->LeadOutDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->GroupPasses->LeadOutLarge);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleLargePasses,MODE_LEADOUT,m_scLinkParams->GroupPasses->UseDefaultLarge,false);
		break;
	case 2:
		if( *m_scLinkParams->LeadInDefault == *m_scLinkParams->GroupPasses->LeadInLarge &&
			*m_scLinkParams->LeadOutDefault == *m_scLinkParams->GroupPasses->LeadOutLarge )
			m_scLinkParams->GroupPasses->UseDefaultLarge = true;
		if( m_scLinkParams->GroupPasses->UseDefaultLarge )
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->LeadInDefault);
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->LeadOutDefault);
		}
		else
		{
			m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->GroupPasses->LeadInLarge);
			m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->GroupPasses->LeadOutLarge);
		}
		m_LinkParamsDlg2->SetMode(m_LeadInOutTitleLargePasses,MODE_BOTH,m_scLinkParams->GroupPasses->UseDefaultLarge,false);
		break;
	}
	System::Windows::Forms::DialogResult dlgRes = m_LinkParamsDlg2->ShowDialog(this);
	if (dlgRes == System::Windows::Forms::DialogResult::OK)
	{
		m_scLinkParams->GroupPasses->UseDefaultLarge = m_LinkParamsDlg2->GetUseDefaults();
		switch( m_cbLargePassesUse->SelectedIndex )
		{
		case 0:
			if( *m_scLinkParams->LeadInDefault == *m_LinkParamsDlg2->m_scParamsLeadIn )
				m_scLinkParams->GroupPasses->UseDefaultLarge = true;
			if( m_scLinkParams->GroupPasses->UseDefaultLarge )
			{
				*m_scLinkParams->GroupPasses->LeadInLarge = *m_scLinkParams->LeadInDefault;
			}
			else
			{
				*m_scLinkParams->GroupPasses->LeadInLarge = *m_LinkParamsDlg2->m_scParamsLeadIn;
			}
			break;
		case 1:
			if( *m_scLinkParams->LeadOutDefault == *m_LinkParamsDlg2->m_scParamsLeadOut )
				m_scLinkParams->GroupPasses->UseDefaultLarge = true;
			if( m_scLinkParams->GroupPasses->UseDefaultLarge )
			{
				*m_scLinkParams->GroupPasses->LeadOutLarge = *m_scLinkParams->LeadOutDefault;
			}
			else
			{
				*m_scLinkParams->GroupPasses->LeadOutLarge = *m_LinkParamsDlg2->m_scParamsLeadOut;
			}
			break;
		case 2:
			if( *m_scLinkParams->LeadInDefault == *m_LinkParamsDlg2->m_scParamsLeadIn &&
				*m_scLinkParams->LeadOutDefault == *m_LinkParamsDlg2->m_scParamsLeadOut )
				m_scLinkParams->GroupPasses->UseDefaultLarge = true;
			if( m_scLinkParams->GroupPasses->UseDefaultLarge )
			{
				*m_scLinkParams->GroupPasses->LeadInLarge = *m_scLinkParams->LeadInDefault;
				*m_scLinkParams->GroupPasses->LeadOutLarge = *m_scLinkParams->LeadOutDefault;
			}
			else
			{
				*m_scLinkParams->GroupPasses->LeadInLarge = *m_LinkParamsDlg2->m_scParamsLeadIn;
				*m_scLinkParams->GroupPasses->LeadOutLarge = *m_LinkParamsDlg2->m_scParamsLeadOut;
			}
			break;
		}
	}
} 

DEFINE_CONTROL_HANDLER( m_btnLinkParamsDefaults_Click )
{ 
	LinkParamsLeadsDlg ^ m_LinkParamsDlg2 = gcnew LinkParamsLeadsDlg(m_ResourceManager,this);
	m_LinkParamsDlg2->SetMode(m_LeadInOutTitleDefaults,MODE_BOTH,false,true);

	m_LinkParamsDlg2->m_scParamsLeadIn = gcnew LeadsParams(m_scLinkParams->LeadInDefault);
	m_LinkParamsDlg2->m_scParamsLeadOut = gcnew LeadsParams(m_scLinkParams->LeadOutDefault);

	System::Windows::Forms::DialogResult dlgRes = m_LinkParamsDlg2->ShowDialog(this);
	if (dlgRes == System::Windows::Forms::DialogResult::OK)
	{
		*m_scLinkParams->LeadInDefault = *m_LinkParamsDlg2->m_scParamsLeadIn;
		*m_scLinkParams->LeadOutDefault = *m_LinkParamsDlg2->m_scParamsLeadOut;
	}
} 
DEFINE_CONTROL_HANDLER( m_btnLeadsFirstEntry_Enter ){CALC_FIRST_ENTRY_IMAGE}
DEFINE_CONTROL_HANDLER( m_btnLeadsLastExit_Enter ){CALC_LAST_EXIT_IMAGE}
DEFINE_CONTROL_HANDLER( m_btnLeadsSmallGaps_Enter ){CALC_GAP_MOVE_HANDLING_IMAGE( m_cbSmallGaps );}
DEFINE_CONTROL_HANDLER( m_btnLeadsLargeGaps_Enter ){CALC_GAP_MOVE_HANDLING_IMAGE( m_cbLargeGaps );}
DEFINE_CONTROL_HANDLER( m_btnLeadsSmallSlices_Enter ){CALC_SLICE_MOVE_HANDLING_IMAGE( m_cbSmallSlices );}
DEFINE_CONTROL_HANDLER( m_btnLeadsLargeSlices_Enter ){CALC_SLICE_MOVE_HANDLING_IMAGE( m_cbLargeSlices );}
DEFINE_CONTROL_HANDLER( m_btnLeadsSmallPasses_Enter ){CALC_PASS_MOVE_HANDLING_IMAGE( m_cbSmallPasses );}
DEFINE_CONTROL_HANDLER( m_btnLeadsLargePasses_Enter ){CALC_PASS_MOVE_HANDLING_IMAGE( m_cbLargePasses );}
DEFINE_CONTROL_HANDLER( m_btnLinkParamsDefaults_Enter ){} 








