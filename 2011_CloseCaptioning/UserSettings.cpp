#include "UserSettings.h"

#define SYSID PCLCCPDS
#define CLASS "UserSettings"
#define LOG_ENABLE 1
//#define LOG_DISABLE_TRACE 1
//#define LOG_DISABLE_DEBUG 1
//#define LOG_DISABLE_INFO 1
//#define LOG_DISABLE_WARNING 1
#include "debug.h"

//#define DEBUG_VERBOSE 1

std::auto_ptr<UserSettings>UserSettings::Instance;

UserSettings * UserSettings::GetInstance() throw ()
{
  if (Instance.get() == NULL) {
    Instance.reset(new UserSettings);
  }
  return Instance.get();
}

UserSettings::UserSettings() :
  ServiceBitField(0)
{
  UIOptions.RenderingState = STATE_DISABLE;
  UIOptions.Setting = SETTING_AUTO;
  UIOptions.Digital = DIGITAL_PRIMARYLANG;
//  UIOptions.Digital = DIGITAL_SECONDARYLANG;
  UIOptions.Analog = ANALOG_CC1;
  UIOptions.FGColor = COLOR_AUTO;
  UIOptions.FGOpacity = OPACITY_AUTO;
  UIOptions.BGColor = COLOR_AUTO;
  UIOptions.BGOpacity = OPACITY_AUTO;
  UIOptions.Font = FONTSTYLE_AUTO;
  UIOptions.EdgeColor = COLOR_AUTO;
  UIOptions.Edge = EDGE_AUTO;
  UIOptions.PenSize = PENSIZE_AUTO;
  UIOptions.Italic = ITALIC_AUTO;
  UIOptions.Underline = UNDERLINE_AUTO;
  UIOptions.WindowColor = COLOR_AUTO;
  UIOptions.BorderColor = COLOR_AUTO;
  UIOptions.WindowOpacity = OPACITY_AUTO;
  UIOptions.WindowEdge = EDGE_AUTO;
}

UserSettings::~UserSettings()
{
}

void UserSettings::Initialize()
{
  //void ErrorRet = ERR_NO_ERROR;
  //if (ERR_NO_ERROR == ErrorRet) {
    // Setup the 608 decoder
    //Update608DecoderParameters(UIOptions.Analog);
  //}
  //return (ErrorRet);
}

bool UserSettings::ValidateService(uint8_t service)
{
#ifdef DEBUG_VERBOSE
    LINFO("ValidateService current digital is: %d", UIOptions.Digital);
#endif
  //Check if the service has not been previously detected
  if ((ServiceBitField & (1 << service)) == 0) {
    // Mark service 0-7 as detected
    ServiceBitField |= (uint8_t)(1 << service);
  }
  return ((DigitalTypes)service == (DigitalTypes)UIOptions.Digital);
}

void UserSettings::InsertUserSettings(WindowAttr * attr,
    PenAttr * pPenAttributes, PenColorAttr * penColor)
{
#ifdef DEBUG_VERBOSE
    LINFO("InsertUserSettings");
#endif
  //Only allow changes if the user has selected "USER" for the Settings option
  if (SETTING_USER == UIOptions.Setting) {
    if (attr) {
      if (COLOR_AUTO != UIOptions.WindowColor) {
        attr->FillColor = ToRGBA(UIOptions.WindowColor);
      }
      if (COLOR_AUTO != UIOptions.BorderColor) {
        attr->BorderColor = ToRGBA(UIOptions.BorderColor);
      }
      if (OPACITY_AUTO != UIOptions.WindowOpacity) {
        attr->FillOpacity = UIOptions.WindowOpacity;
      }
      if (EDGE_AUTO != UIOptions.WindowEdge) {
        //attr->BorderType =
        //(BorderTypes)GetCCGSEdgeType(UIOptions.WindowEdge);
      }
    }
    if (pPenAttributes) {
      if (PENSIZE_AUTO != UIOptions.PenSize) {
        pPenAttributes->Size = UIOptions.PenSize;
      }
      if (FONTSTYLE_AUTO != UIOptions.Font) {
        pPenAttributes->Font = GetCCGSFontStyle();
      }
      if (EDGE_AUTO != UIOptions.Edge) {
        pPenAttributes->Edge = UIOptions.Edge;
      }
      if (ITALIC_AUTO != UIOptions.Italic) {
        if (ITALIC_ON == UIOptions.Italic) {
          pPenAttributes->IsItalic = true;
        }
        else if (ITALIC_OFF == UIOptions.Italic) {
          pPenAttributes->IsItalic = false;
        }
      }
      if (UNDERLINE_AUTO != UIOptions.Underline) {
        if (UNDERLINE_ON == UIOptions.Underline) {
          pPenAttributes->Underline = true;
        }
        else if (UNDERLINE_OFF == UIOptions.Underline) {
          pPenAttributes->Underline = false;
        }
      }
    }
    if (penColor) {
      if (COLOR_AUTO != UIOptions.FGColor) {
        penColor->FG = ToRGBA(UIOptions.FGColor);
      }
      if (OPACITY_AUTO != UIOptions.FGOpacity) {
        penColor->FGOpacity = UIOptions.FGOpacity;
      }
      if (COLOR_AUTO != UIOptions.EdgeColor) {
        penColor->Edge = ToRGBA(UIOptions.EdgeColor);
      }
      if (COLOR_AUTO != UIOptions.BGColor) {
        penColor->BG = ToRGBA(UIOptions.BGColor);
      }
      if (OPACITY_AUTO != UIOptions.BGOpacity) {
        penColor->BGOpacity = UIOptions.BGOpacity;
      }
    }
  }
  //return (ERR_NO_ERROR);
}

void UserSettings::UserStatusGet(UIState *pCCUserOptionsStatus)
{
#ifdef DEBUG_VERBOSE
    LINFO("UserStatusGet");
#endif
  //void ErrorRet = ERR_NO_ERROR;
  if (NULL == pCCUserOptionsStatus) {
    //ErrorRet = ERR_NULL_PTR;
  }
  else {
    pCCUserOptionsStatus->RenderingState = UIOptions.RenderingState;
    pCCUserOptionsStatus->PenSize = UIOptions.PenSize;
    pCCUserOptionsStatus->Font = UIOptions.Font;
    pCCUserOptionsStatus->FGColor = UIOptions.FGColor;
    pCCUserOptionsStatus->EdgeColor = UIOptions.EdgeColor;
    pCCUserOptionsStatus->Edge = UIOptions.Edge;
    pCCUserOptionsStatus->Analog = UIOptions.Analog;
    pCCUserOptionsStatus->FGOpacity = UIOptions.FGOpacity;
    pCCUserOptionsStatus->BGColor = UIOptions.BGColor;
    pCCUserOptionsStatus->BGOpacity = UIOptions.BGOpacity;
    pCCUserOptionsStatus->Digital = UIOptions.Digital;
    pCCUserOptionsStatus->Setting = UIOptions.Setting;
    //pCCUserOptionsStatus->Stream = GetCCStreamTypePresent();
    pCCUserOptionsStatus->Italic = UIOptions.Italic;
    pCCUserOptionsStatus->Underline = UIOptions.Underline;
    pCCUserOptionsStatus->BorderColor = UIOptions.BorderColor;
    pCCUserOptionsStatus->WindowEdge = UIOptions.WindowEdge;
  }
  //return ErrorRet;
}

void UserSettings::SettingsRestore(void)
{
#ifdef DEBUG_VERBOSE
    LINFO("SettingsRestore");
#endif
  //void ErrorRet = ERR_NO_ERROR;
  bool bChanged = false;

  //for all settings if the value changed, update the local copy and NVMEM
  if (UIOptions.RenderingState != STATE_DISABLE) {
    UIOptions.RenderingState = STATE_DISABLE;
    bChanged = true;
  }
  if (UIOptions.PenSize != PENSIZE_AUTO) {
    UIOptions.PenSize = PENSIZE_AUTO;
    bChanged = true;
  }
  if (UIOptions.EdgeColor != COLOR_AUTO) {
    UIOptions.EdgeColor = COLOR_AUTO;
    bChanged = true;
  }
  if (UIOptions.Edge != EDGE_AUTO) {
    UIOptions.Edge = EDGE_AUTO;
    bChanged = true;
  }
  if (UIOptions.Analog != ANALOG_CC1) {
    UIOptions.Analog = ANALOG_CC1;
    bChanged = true;
  }
  if (UIOptions.Font != FONTSTYLE_AUTO) {
    UIOptions.Font = FONTSTYLE_AUTO;
    bChanged = true;
  }
  if (UIOptions.FGColor != COLOR_AUTO) {
    UIOptions.FGColor = COLOR_AUTO;
    bChanged = true;
  }
  if (UIOptions.FGOpacity != OPACITY_AUTO) {
    UIOptions.FGOpacity = OPACITY_AUTO;
    bChanged = true;
  }
  if (UIOptions.BGColor != COLOR_AUTO) {
    UIOptions.BGColor = COLOR_AUTO;
    bChanged = true;
  }
  if (UIOptions.BGOpacity != OPACITY_AUTO) {
    UIOptions.BGOpacity = OPACITY_AUTO;
    bChanged = true;
  }
//  if (UIOptions.Digital != DIGITAL_SECONDARYLANG) {
//    UIOptions.Digital = DIGITAL_SECONDARYLANG;
//    bChanged = true;
//  }
  if (UIOptions.Digital != DIGITAL_PRIMARYLANG) {
    UIOptions.Digital = DIGITAL_PRIMARYLANG;
    bChanged = true;
  }
  if (UIOptions.Setting != SETTING_AUTO) {
    UIOptions.Setting = SETTING_AUTO;
    bChanged = true;
  }
  if (UIOptions.Italic != ITALIC_AUTO) {
    UIOptions.Italic = ITALIC_AUTO;
    bChanged = true;
  }
  if (UIOptions.Underline != UNDERLINE_AUTO) {
    UIOptions.Underline = UNDERLINE_AUTO;
    bChanged = true;
  }
  if (bChanged) {
    //Update608DecoderParameters(UIOptions.Analog);
  }
  //return ErrorRet;
}

FontTypes UserSettings::GetCCGSFontStyle(void)
{
  FontTypes tempFont = FONT_0;
  switch (UIOptions.Font) {
  case FONTSTYLE_DEFAULT:
    tempFont = FONT_0;
    break;
  case FONTSTYLE_MONOSERIF:
    tempFont = FONT_1;
    break;
  case FONTSTYLE_PROPORTIONSERIF:
    tempFont = FONT_2;
    break;
  case FONTSTYLE_MONONOSERIF:
    tempFont = FONT_3;
    break;
  case FONTSTYLE_PROPORTIONNOSERIF:
    tempFont = FONT_4;
    break;
  case FONTSTYLE_CASUAL:
    tempFont = FONT_5;
    break;
  case FONTSTYLE_CURSIVE:
    tempFont = FONT_6;
    break;
  case FONTSTYLE_SMALL:
    tempFont = FONT_7;
    break;
  case FONTSTYLE_AUTO : //fallthrough
  case FONTSTYLE_INVALID:
  case FONTSTYLE_FORCE_uint32_t:
  default:
    tempFont = FONT_0;
    break;
  }
  return tempFont;
}

uint8_t UserSettings::ToRGBA(Colors color)
{
  //RGB 332 color space
  return ((RGBAColor)color);
  switch (color) {
  case COLOR_BLACK: // Black
    return 0x00;
  case COLOR_WHITE: // White
    return 0xff;
  case COLOR_RED: // Red
    return 0xE0;
  case COLOR_GREEN: // Green
    return 0x1C;
  case COLOR_BLUE: // Blue
    return 0x03;
  case COLOR_NAVY_BLUE: // Navy Blue
    return 0x02;
  case COLOR_ULTRA_MARINE: // Ultramarine
    return 0xff;
  case COLOR_DARK_BLUE: // Dark Blue
    return 0xff;
  case COLOR_TEAL: // Teal
    return 0xff;
  case COLOR_BONDI_BLUE: // Bondi Blue
    return 0xff;
  case COLOR_AZURE: // Azure
    return 0xff;
  case COLOR_LEAF_GREEN: // Leaf Green
    return 0xff;
  case COLOR_CARIBBEAN: // Caribbean
    return 0xff;
  case COLOR_ROBIN_EGG_BLUE: // Robin Egg Blue
    return 0xff;
  case COLOR_AQUA: // Aqua
    return 0xff;
  case COLOR_SPRING_GREEN: // Spring Green
    return 0xff;
  case COLOR_BRIGHT_TURQUOISE: // Turquoise
    return 0xff;
  case COLOR_CYAN: // Cyan
    return 0xff;
  case COLOR_MAROON: // Maroon
    return 0xff;
  case COLOR_EGGPLANT: // Eggplant
    return 0xff;
  case COLOR_VIOLET: // Violet
    return 0xff;
  case COLOR_PERSIAN_BLUE: // Persian Blue
    return 0xff;
  case COLOR_OLIVE: // Olive
    return 0xff;
  case COLOR_GRAY: // Gray
    return 0xff;
  case COLOR_DENIM_BLUE: // Denim Blue
    return 0xff;
  case COLOR_AMETHYST: // Amethyst
    return 0xff;
  case COLOR_HUNTER_GREEN: // Hunter Green
    return 0xff;
  case COLOR_PALE_SEA_GREEN: // Sea Green
    return 0xff;
  case COLOR_PALE_CYAN: // Pale Cyan
    return 0xff;
  case COLOR_CORN_FLOWER_BLUE: // Cornflower Blue
    return 0xff;
  case COLOR_CHARTREUSE: // Chartreuse
    return 0xff;
  case COLOR_LIGHT_GREEN: // Light Green
    return 0xff;
  case COLOR_AQUAMARINE: // Aquamarine
    return 0xff;
  case COLOR_PALE_SKY_BLUE: // Pale Sky Blue
    return 0xff;
  case COLOR_SANGRIA: // Sangria
    return 0xff;
  case COLOR_RED_VIOLET: // Red Violet
    return 0xff;
  case COLOR_DARK_MAGENTA: // Dark Magenta
    return 0xff;
  case COLOR_PURPLE: // Purple
    return 0xff;
  case COLOR_DARK_GOLDENROD: // Dark Goldenrod
    return 0xff;
  case COLOR_PUCE: // Puce
    return 0xff;
  case COLOR_LILAC: // Lilac
    return 0xff;
  case COLOR_HELIOTROPE: // Heliotrope
    return 0xff;
  case COLOR_GOLDENROD: // Goldenrod
    return 0xff;
  case COLOR_DARK_KHAKI: // Dark Khaki
    return 0xff;
  case COLOR_SILVER: // Silver
    return 0xff;
  case COLOR_PERIWINKLE: // Periwinkle
    return 0xff;
  case COLOR_LIME: // Lime
    return 0xff;
  case COLOR_MINT_GREEN: // Mint Green
    return 0xff;
  case COLOR_GRAY_TEA_GREEN: // Tea Green
    return 0xff;
  case COLOR_PALE_BLUE: // Pale Blue
    return 0xff;
  case COLOR_FUSCHIA: // Fuschia
    return 0xff;
  case COLOR_HOT_PINK: // Hot Pink
    return 0xff;
  case COLOR_MAGENTA: // Magenta
    return 0xff;
  case COLOR_ORANGE: // Orange
    return 0xff;
  case COLOR_CORAL: // Coral
    return 0xff;
  case COLOR_PALE_MAGENTA: // Pale Magenta
    return 0xff;
  case COLOR_ORCHID: // Orchid
    return 0xff;
  case COLOR_AMBER: // Amber
    return 0xff;
  case COLOR_PALE_ORANGE: // Pale Orange
    return 0xff;
  case COLOR_PINK: // Pink
    return 0xff;
  case COLOR_PALE_PURPLE: // Pale Purple
    return 0xff;
  case COLOR_YELLOW: // Yellow
    return 0xff;
  case COLOR_PALE_YELLOW: // Pale Yellow
    return 0xff;
  case COLOR_LEMON_CREAM: // Lemon Cream
    return 0xff;
  case COLOR_AUTO: // Background Color is controlled
  case COLOR_INVALID:
  case COLOR_FORCE_uint32_t:
  default:
    return 0;
  }
}

AnalogTypes UserSettings::GetCCGSAnalogCCType()
{
  AnalogTypes AnalogCCTypeOut = ANALOG_CC1;
  switch (UIOptions.Analog) {
  case ANALOG_CC1:
    AnalogCCTypeOut = ANALOG_CC1;
    break;
  case ANALOG_CC2:
    AnalogCCTypeOut = ANALOG_CC2;
    break;
  case ANALOG_CC3:
    AnalogCCTypeOut = ANALOG_CC3;
    break;
  case ANALOG_CC4:
    AnalogCCTypeOut = ANALOG_CC4;
    break;
  case ANALOG_T1:
    AnalogCCTypeOut = ANALOG_T1;
    break;
  case ANALOG_T2:
    AnalogCCTypeOut = ANALOG_T2;
    break;
  case ANALOG_T3:
    AnalogCCTypeOut = ANALOG_T3;
    break;
  case ANALOG_T4:
    AnalogCCTypeOut = ANALOG_T4;
    break;
  case ANALOG_INVALID : //fallthrough
  case ANALOG_FORCE_uint32_t:
  default:
    AnalogCCTypeOut = ANALOG_CC1;
    break;
  }
  return (AnalogCCTypeOut);
}

void UserSettings::SetRenderingState(RenderingStates RenderingState)
{
#ifdef DEBUG_VERBOSE
    LINFO("SetRenderingState: %d", RenderingState);
#endif
  //void ErrorRet = ERR_NO_ERROR;
  if ((RenderingState >= STATE_ENABLE) &&
      (RenderingState < STATE_INVALID)) {
    if (UIOptions.RenderingState != RenderingState) {
      UIOptions.RenderingState = RenderingState;
    }
  }
  else {
    ///ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::PenSizeSet(PenSizes CCPenSize)
{
  //void ErrorRet = ERR_NO_ERROR;

  if ((CCPenSize >= PENSIZE_AUTO) && (CCPenSize < PENSIZE_INVALID)) {
    if (UIOptions.PenSize != CCPenSize) {
      UIOptions.PenSize = CCPenSize;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::FontStyleSet(FontStyles CCFontStyle)
{
  //void ErrorRet = ERR_NO_ERROR;
  if ((CCFontStyle >= FONTSTYLE_AUTO) &&
      (CCFontStyle < FONTSTYLE_INVALID))
  {
    if (UIOptions.Font != CCFontStyle) {
      UIOptions.Font = CCFontStyle;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::FontItalicizedSet(ItalicTypes CCItalicizedType)
{
  //void ErrorRet = ERR_NO_ERROR;
  if ((CCItalicizedType >= ITALIC_AUTO) &&
      (CCItalicizedType < ITALIC_INVALID)) {
    if (UIOptions.Italic != CCItalicizedType) {
      UIOptions.Italic = CCItalicizedType;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::FontUnderlineSet(UnderlineTypes CCUnderlineStyle)
{
  //void ErrorRet = ERR_NO_ERROR;
  if ((CCUnderlineStyle >= UNDERLINE_AUTO) &&
      (CCUnderlineStyle < UNDERLINE_INVALID)) {
    if (UIOptions.Underline != CCUnderlineStyle) {
      UIOptions.Underline = CCUnderlineStyle;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::ForegroundColorSet(Colors CCFgColor)
{
  //void ErrorRet = ERR_NO_ERROR;

  if ((CCFgColor >= COLOR_BLACK) &&
      (CCFgColor < COLOR_INVALID)) {
    if (UIOptions.FGColor != CCFgColor) {
      UIOptions.FGColor = CCFgColor;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::FontEdgeColorSet(
  Colors CCEgColor)
{
  //void ErrorRet = ERR_NO_ERROR;
  if ((CCEgColor >= COLOR_BLACK)
      && (CCEgColor < COLOR_INVALID)) {
    if (UIOptions.EdgeColor != CCEgColor) {
      UIOptions.EdgeColor = CCEgColor;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::FontEdgeTypeSet(
  EdgeTypes CCEgType)
{
  //void ErrorRet = ERR_NO_ERROR;
  if ((CCEgType >= EDGE_AUTO) &&
      (CCEgType < EDGE_INVALID)) {
    if (UIOptions.Edge != CCEgType) {
      UIOptions.Edge = CCEgType;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::AnalogServiceSet(AnalogTypes CCAnalogType)
{
  //void ErrorRet = ERR_NO_ERROR;
  if ((CCAnalogType < ANALOG_INVALID)
      && (CCAnalogType >= ANALOG_CC1)) {
    if (UIOptions.Analog != CCAnalogType) {
      UIOptions.Analog = CCAnalogType;
      //if (ErrorRet == ERR_NO_ERROR) {
        //Update608DecoderParameters(CCAnalogType);
      //}
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::ForegroundOpacitySet(OpacityTypes CCFgOpacity)
{
  //void ErrorRet = ERR_NO_ERROR;
  if ((CCFgOpacity >= OPACITY_AUTO) &&
      (CCFgOpacity < OPACITY_INVALID)) {
    if (UIOptions.FGOpacity != CCFgOpacity) {
      UIOptions.FGOpacity = CCFgOpacity;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::BackgroundColorSet(
  Colors CCBgColor)
{
  //void ErrorRet = ERR_NO_ERROR;
  if ((CCBgColor >= COLOR_BLACK) &&
      (CCBgColor < COLOR_INVALID)) {
    if (UIOptions.BGColor != CCBgColor) {
      UIOptions.BGColor = CCBgColor;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::WindowColorSet(
  Colors eCCwindowColor)
{
  //void ErrorRet = ERR_NO_ERROR;
  if ((eCCwindowColor >= COLOR_BLACK) &&
      (eCCwindowColor < COLOR_INVALID)) {
    if (UIOptions.WindowColor != eCCwindowColor) {
      UIOptions.WindowColor = eCCwindowColor;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::WindowBorderColorSet(Colors eCCwindowBorderColor)
{
  //void ErrorRet = ERR_NO_ERROR;
  if ((eCCwindowBorderColor >= COLOR_BLACK) &&
      (eCCwindowBorderColor < COLOR_INVALID))
  {
    if (UIOptions.BorderColor != eCCwindowBorderColor) {
      UIOptions.BorderColor = eCCwindowBorderColor;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::WindowEdgeTypeSet(EdgeTypes eCCBgEdgeType)
{
  //void ErrorRet = ERR_NO_ERROR;
  if ((eCCBgEdgeType >= EDGE_AUTO) &&
      (eCCBgEdgeType < EDGE_INVALID))
  {
    if (UIOptions.WindowEdge != eCCBgEdgeType) {
      UIOptions.WindowEdge = eCCBgEdgeType;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::WindowOpacitySet(OpacityTypes eCCwindowOpacity)
{
  //void ErrorRet = ERR_NO_ERROR;
  if ((eCCwindowOpacity >= OPACITY_AUTO) &&
      (eCCwindowOpacity < OPACITY_INVALID)) {
    if (UIOptions.WindowOpacity != eCCwindowOpacity) {
      UIOptions.WindowOpacity = eCCwindowOpacity;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::BackgroundOpacitySet(OpacityTypes CCBgOpacity)
{
  //void ErrorRet = ERR_NO_ERROR;
  if ((CCBgOpacity >= OPACITY_AUTO) &&
      (CCBgOpacity < OPACITY_INVALID)) {
    if (UIOptions.BGOpacity != CCBgOpacity) {
      UIOptions.BGOpacity = CCBgOpacity;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::DigitalServiceSet(DigitalTypes CCServSelection)
{
  //void ErrorRet = ERR_NO_ERROR;
  if ((CCServSelection >= DIGITAL_NO_SERVICE) &&
      (CCServSelection < DIGITAL_INVALID)) {
    if (UIOptions.Digital != CCServSelection) {
      UIOptions.Digital = CCServSelection;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

void UserSettings::ControlModeSet(Settings CCSetting)
{
  //void ErrorRet = ERR_NO_ERROR;
  if ((CCSetting >= SETTING_AUTO) &&
      (CCSetting < SETTING_INVALID)) {
    if (UIOptions.Setting != CCSetting) {
      UIOptions.Setting = CCSetting;
    }
  }
  else {
    //ErrorRet = ERR_BAD_PARAMETER;
  }
  //return ErrorRet;
}

