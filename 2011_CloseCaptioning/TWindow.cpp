#include <TWindow.h>

#define SYSID PCLCCGS
#define CLASS "Window"
#define LOG_ENABLE 1
//#define LOG_DISABLE_DEBUG 1
//#define LOG_DISABLE_TRACE 1
//#define LOG_DISABLE_INFO 1
//#define LOG_DISABLE_WARNING 1
#include "debug.h"

//#define DEBUG_VERBOSE 1

#ifdef FLTK_WIDGETS
void TWindow::DrawText(const char * text)
{
  Pixmap->DrawText(text);
}
#endif

TKreaTvGfxVideoMode TWindow::VideoMode;

TKreaTvGfxPointer TWindow::Gfx;
TKreaTvGfxDisplayPointer TWindow::GfxDisplay;
TKreaTvGfxBlitterPointer TWindow::GfxBlitter;
TClosedCaptioningRenderer * TWindow::Renderer = 0;

TWindow::TWindow() :
  X(0),
  Y(0),
  Width(0),
  Height(0),
  CurrentColumn(0),
  CurrentRow(0),
  PreviousRow(0),
  NewLine(false),
  Flush(false)
{
  memset(&Definition, 0, sizeof(Definition));
  memset(&Attributes, 0, sizeof(Attributes));
  memset(&PenAttributes, 0, sizeof(PenAttributes));
  memset(&PenColor, 0, sizeof(PenColor));
  Definition.ID = INVALID;
}

TWindow::~TWindow()
{
}

bool TWindow::IsValid()
{
  //return Definition.ID != INVALID && Pixmap.get();
  return Pixmap.get();
}

void TWindow::Clear()
{
#ifdef DEBUG_VERBOSE
  LWARNING("Clear: ID: ^%d^ - xy: %d:%d wh: %d:%d",
      Definition.ID, X, Y, Width, Height);
#endif
  RenderChar(0x02);
  PreviousRow = 1;
  CurrentRow = 1;
  CurrentColumn = 1;
  Font.SetPosition(1, 1);
  ClearPixmap();
  Update();
}

void TWindow::Delete()
{
#ifdef DEBUG_VERBOSE
  LWARNING("Delete: ID: ^%d^ - xy: %d:%d wh: %d:%d",
      Definition.ID, X, Y, Width, Height);
#endif
  RenderChar(0x02);
  SetVisible(false);
  PreviousRow = 1;
  CurrentRow = 1;
  CurrentColumn = 1;
  Font.SetPosition(1, 1);
  ClearPixmap();
  Update();
  SetInvalid();
}

void TWindow::Display()
{
#ifdef DEBUG_VERBOSE
  LWARNING("Display: ID: ^%d^ - xy: %d:%d wh: %d:%d",
      Definition.ID, X, Y, Width, Height);
#endif
  RenderChar(0x02);
  SetVisible(true);
  Update();
}

void TWindow::Hide()
{
#ifdef DEBUG_VERBOSE
  LWARNING("Hide: ID: ^%d^ - xy: %d:%d wh: %d:%d",
      Definition.ID, X, Y, Width, Height);
#endif
  RenderChar(0x02);
  SetVisible(false);
  Update();
}

void TWindow::Toggle()
{
#ifdef DEBUG_VERBOSE
  LWARNING("Toggle: ID: ^%d^ - xy: %d:%d wh: %d:%d",
      Definition.ID, X, Y, Width, Height);
#endif
  RenderChar(0x02);
  SetVisible(!IsVisible());
  Update();
}

bool TWindow::Create()
{
#ifdef DEBUG_VERBOSE
  LINFO("CreateWindow: ID: ^%d^", Definition.ID);
#endif
  if (Pixmap.get()) {
    Pixmap.reset();
  }
  CreatePixmap(KGFX_PIXEL_FORMAT_ARGB8888);
  if (IsValid()) {
#ifdef DEBUG_VERBOSE
//    LDEBUG("CreateWindow: Window created");
#endif
  }
  else {
#ifdef DEBUG_VERBOSE
    LERROR("CreateWindow: Could not create a window");
#endif
    return false;
  }
  return true;
}

void TWindow::RenderChar(uint8_t c)
{
#ifdef MOCK_OBJECTS
  Renderer->ShowText((const char *)&c);
#endif
#ifdef DEBUG_VERBOSE
  LTRACE("RenderChar: ID: ^%d^ - char: %c", Definition.ID, c);
#endif
  switch (c) {
  case 0x02: //start of text, set by SetPenLocation
//    LWARNING("RenderChar: Flush");
    Flush = true;
    break;
  case 0x03: //end of text, force new line
//    LWARNING("RenderChar: Flush END OF TEXT!!!");
    Flush = true;
    break;
  case 0x08: //backspace
    CurrentColumn--;
    Font.Backspace();
#ifdef DEBUG_VERBOSE
//    LDEBUG("RenderChar: Backspace");
#endif
    return;
  }
  if ((c >= 0x11) && (c <= 0x1F)) {
    // Added G2 character offset.
    c += 0x10;
  }
  if (c == 0x0D || c == 0x0A) {
    //force a new line, next character
    NewLine = true;
#ifdef DEBUG_VERBOSE
    LDEBUG("RenderChar: new line");
#endif
  }
  switch (c) {
  case 0x02:
  case 0x03:
  case 0x0D:
  case 0x0A:
//    LWARNING("RenderChar: Flush or EOL, exiting..");
    return;
  }
  if (c < ' ') {
#ifdef DEBUG_VERBOSE
//    LERROR("RenderChar: invalid char, exit()");
#endif
    return;
  }
  if (CurrentColumn > Definition.Columns) {
    NewLine = true;
  }
//  if (Flush || NewLine) {
  if (NewLine) {
#ifdef DEBUG_VERBOSE
    LDEBUG("RenderChar: last column, new line");
#endif
    //if SetPenLocation changed current row, how much should we scroll.
    int rowsToScroll = CurrentRow - PreviousRow;
    if (rowsToScroll < 1) {
      rowsToScroll = 1;
    }
    if (rowsToScroll >= (int)Definition.Rows) {
      rowsToScroll = Definition.Rows - 1;
    }
//    LWARNING("RenderChar: rowsToScroll %d", rowsToScroll);
    if (NewLine) {
//      LWARNING("RenderChar: new line, calculating row...");
      CurrentColumn = 1;
      PreviousRow = CurrentRow;
      CurrentRow++;
      if (CurrentRow > Definition.Rows) {
        CurrentRow = Definition.Rows;
      }
    }
    if (CurrentRow > 1) {
      ScrollRegion(0, 0, Width, Height, 0,
          (-Font.GetRowHeight() * rowsToScroll));
    }
//    LWARNING("RenderChar: SetPosition row %d col %d",
//        CurrentRow, CurrentColumn);
//    Font.ClearRowsFromCurrentLocation();
    Font.SetPosition(CurrentRow, CurrentColumn);
    for (uint32_t i = CurrentColumn; i <= Definition.Columns; ++i) {
      Font.CharDraw(' ');
    }
    Font.SetPosition(CurrentRow, CurrentColumn);
    Update();
  }
  Chars[CurrentColumn] = c;
  Font.CharDraw(c);
  CurrentColumn++;
  //if (!isalnum(c) || NewLine || Flush) {
    Update();
  //}
  NewLine = false;
  Flush = false;
}

void TWindow::Update()
{
#ifdef DEBUG_VERBOSE
  //LINFO("UpdateWindow: ID: ^%d^", Definition.ID);
#endif
  if (Pixmap.get()) {
    bool clear = false;
    if (Definition.ID == INVALID || !Definition.IsVisible) {
      clear = true;
    }
//    Border();
    Renderer->UpdateView(Pixmap, LocationRect, clear);
  }
//  else {
//#ifdef DEBUG_VERBOSE
//    LERROR("Update: Invalid pixmap");
//#endif
//  }
}

