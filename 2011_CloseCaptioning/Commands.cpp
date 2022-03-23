#include "Commands.h"
#include <IDisplay.h>
#include <WindowDefinitions.h>
#include <cstring>

#define SYSID PCLCCPDS
#define CLASS "Commands"
#define LOG_ENABLE 1
//#define LOG_DISABLE_TRACE 1
//#define LOG_DISABLE_DEBUG 1
//#define LOG_DISABLE_INFO 1
//#define LOG_DISABLE_WARNING 1
#define LOG_DISABLE_COMMAND_OUTPUT 1
#include "debug.h"

//#define DEBUG_VERBOSE 1
#define DEBUG_OUTPUT_CCTEXT 1

#define DEFINE_COMMAND1( name ) \
  void Commands::name(uint8_t param) { \
    LCOMMAND(#name); \
    if (gDisplay != NULL) { \
      gDisplay->Command##name(param); \
    } \
  }

#define DEFINE_COMMAND0( name ) \
  void Commands::name() { \
    LCOMMAND(#name); \
    if (gDisplay != NULL) { \
      gDisplay->Command##name(); \
    } \
  }

#define DEFINE_COMMAND2( name, p1name, p1val ) \
  void Commands::name(p1name p1val) { \
    LCOMMAND(#name); \
    if (gDisplay != NULL) { \
      gDisplay->Command##name(p1val); \
    } \
  }

std::auto_ptr<Commands>Commands::Instance;
static IDisplay * gDisplay;

Commands * Commands::GetInstance() throw ()
{
  if (Instance.get() == NULL) {
    Instance.reset(new Commands);
  }
  return Instance.get();
}

Commands::Commands()
{
  if (gDisplay == NULL) {
    gDisplay = IDisplay::Instance();
    if (gDisplay == NULL) {
      LERROR("Couldn't get instance of CCGS");
    }
  }
}

void Commands::WriteChar(uint8_t c)
{
#ifdef DEBUG_VERBOSE
  LCOMMAND("\t\t\t *** %c ***", c);
  //usleep(90000);
#endif
  if (gDisplay != NULL) {
    gDisplay->WriteChar(c);
  }
#ifdef DEBUG_OUTPUT_CCTEXT
  static uint8_t buf[82];
  static uint32_t  sz = 0;
  bool line_break = false;
  switch (c) {
  case '\r':
  case '\n':
    c = ' ';
    line_break = true;
    break;
  case '\0':
    c = '0';
    break;
  default:
    break;
  }
  uint32_t max_chars = 40;
  buf[sz++] = c;
  if (sz >= max_chars || line_break) {
    buf[sz++] = 0;
    LTEXT("CCText: %s", buf);
    sz = 0;
  }
#endif
}

void Commands::DefineWindowAsCurrent(WindowDef& winDef,
    WindowAttr& winAttr, PenAttr& penAttr, PenColorAttr& penColor)
{
  LCOMMAND("DefineWindowAsCurrent");
  if (gDisplay != NULL) {
    gDisplay->CommandDefineWindowAsCurrent(winDef, winAttr, penAttr, penColor);
  }
}

void Commands::SetPenLocation(uint32_t row, uint32_t col)
{
  LCOMMAND("SetPenLocation");
  if (gDisplay != NULL) {
    gDisplay->CommandSetPenLocation(row, col);
  }
}

DEFINE_COMMAND2( SetWindowAttributes, WindowAttr&, winAttr )
DEFINE_COMMAND2( SetPenAttributes, PenAttr&, penAttr )
DEFINE_COMMAND2( SetPenColor, PenColorAttr&, penColor )
DEFINE_COMMAND1( SetCurrentWindow )
DEFINE_COMMAND1( ClearWindows )
DEFINE_COMMAND1( DisplayWindows )
DEFINE_COMMAND1( HideWindows )
DEFINE_COMMAND1( ToggleWindows )
DEFINE_COMMAND1( DeleteWindows )
DEFINE_COMMAND1( Delay )
DEFINE_COMMAND0( DelayCancel )
DEFINE_COMMAND0( Reset )
DEFINE_COMMAND0( Flush )


