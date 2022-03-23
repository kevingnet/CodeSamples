#include <EIA708Packet.h>

#define SYSID PCLCCPDS
#define CLASS "EIA708Packet"
#define LOG_ENABLE 1
//#define LOG_DISABLE_TRACE 1
//#define LOG_DISABLE_DEBUG 1
//#define LOG_DISABLE_INFO 1
//#define LOG_DISABLE_WARNING 1
#include "debug.h"

#define DEBUG_VERBOSE 1

static const uint8_t HEADER_OFFSET = 0x00;
static const uint8_t CC1_OFFSET = 0x00;
static const uint8_t CC2_OFFSET = 0x01;
static const uint8_t SEQUENCE_MASK = 0xC0;
static const uint8_t SEQUENCE_BITS = 0x06;
static const uint8_t SIZE_MASK = 0x3F;

bool EIA708Packet::Initialize(const uint8_t * data)
{
#ifndef DEBUG_VERBOSE
  ServiceNumber = DEFAULT_SERVICE;
  CompleteLength = (data[HEADER_OFFSET] & SIZE_MASK) * 2;
  if (CompleteLength == 0) {
    CompleteLength = MAX_PACKET_SIZE;
  }
  StoredLength = 0;
  memset(Data, 0, MAX_PACKET_SIZE);
  Data[StoredLength++] = data[CC1_OFFSET];
  Data[StoredLength++] = data[CC2_OFFSET];
  HasData = true;
#else
  LINFO("Initialize: '%c' '%c' '%c'", data[0], data[1], data[2]);
  ServiceNumber = DEFAULT_SERVICE;
  CompleteLength = (data[HEADER_OFFSET] & SIZE_MASK) * 2;
  //HACK! if CompleteLength is not zero, there was line 21 info...
  //if (CompleteLength) {
  //  uint8_t len = CompleteLength;
  //  CompleteLength = (data[HEADER_OFFSET] & SIZE_MASK) * 2;
  //  CompleteLength -= (0xff - len);
  //  CompleteLength --;
  //}
  //else {
  //  CompleteLength = (data[HEADER_OFFSET] & SIZE_MASK) * 2;
  //}
  if (CompleteLength == 0) {
    LWARNING("Initialize"
        "packet len was zero setting to: %d", MAX_PACKET_SIZE);
    CompleteLength = MAX_PACKET_SIZE;
  }
//NICE HACK!!!
//if (CompleteLength == 20) {
//  CompleteLength = 16;
//}
  LDEBUG("Initialize: CompleteLength: %d", CompleteLength);
  StoredLength = 0;
  memset(Data, 0, MAX_PACKET_SIZE);
  Data[StoredLength++] = data[CC1_OFFSET];
  Data[StoredLength++] = data[CC2_OFFSET];
  //Store(data);
  LDEBUG("Initialize: StoredLength: %d", StoredLength);
  HasData = true;
#endif
  return true;
}

bool EIA708Packet::Store(const uint8_t * data)
{
  bool result = false;
#ifndef DEBUG_VERBOSE
  HasData = StoredLength > 0;
  if (StoredLength < CompleteLength) {
    Data[StoredLength++] = data[CC1_OFFSET];
    result = true;
  }
  if (StoredLength < CompleteLength) {
    Data[StoredLength++] = data[CC2_OFFSET];
    result = true;
  }
#else
  LINFO("Store: StoredLength: %d - '%c' '%c'", StoredLength, data[0], data[1]);
  HasData = (bool)StoredLength;
  if (StoredLength < CompleteLength) {
    LDEBUG("Store: CC1: '%c'", data[CC1_OFFSET]);
    Data[StoredLength++] = data[CC1_OFFSET];
  }
  else {
    LDEBUG("Store: not enough room: %d", StoredLength);
  }
  if (StoredLength < CompleteLength) {
    LDEBUG("Store: CC2: '%c'", data[CC2_OFFSET]);
    Data[StoredLength++] = data[CC2_OFFSET];
  }
  else {
    LDEBUG("Store: not enough room: %d", StoredLength);
  }
  LDEBUG("Store: StoredLength: %d", StoredLength);
#endif
  return result;
}

void EIA708Packet::ProcessNextService()
{
  if (ServiceNumber < StoredLength) {
    Service.Initialize(&Data[ServiceNumber]);
    if(Service.IsValid())
    {
      ServiceNumber = Service.UpdateServiceNumber(ServiceNumber);
      if (ServiceNumber > StoredLength) {
        LWARNING("Invalid service block");
      }
    }
    //else {
    //  if (Service.HasSomeData()) {
    //    LWARNING("ProcessNextService: Invalid Service: %d", ServiceNumber);
    //    LWARNING("ServiceNumber: %d ExtensionNumber: %d PacketSize: %d",
    //      Service.GetServiceNumber(), Service.GetExtensionNumber(),
    //      Service.GetPacketSize());
    //  }
    //}
  }
#if 0
#ifndef DEBUG_VERBOSE
  if (ServiceNumber < StoredLength) {
    Service.Initialize(&Data[ServiceNumber]);
    if (Service.IsValid()) {
//      ServiceNumber = Service.UpdateServiceNumber(ServiceNumber);
    }
    else {
      if (Service.HasSomeData()) {
        LWARNING("ProcessNextService: Invalid Service: %d", ServiceNumber);
        LWARNING("ServiceNumber: %d ExtensionNumber: %d PacketSize: %d",
          Service.GetServiceNumber(), Service.GetExtensionNumber(),
          Service.GetPacketSize());
      }
//      else {
//        LWARNING("Service has no data");
//      }
    }
    ServiceNumber = Service.UpdateServiceNumber(ServiceNumber);
  }
#else
  LINFO("ProcessNextService");
  if (ServiceNumber < StoredLength) {
    LDEBUG("ProcessNextService: Initializing with "
        "ServiceNumber: %d", ServiceNumber);
    Service.Initialize(&Data[ServiceNumber]);
    ServiceNumber = Service.UpdateServiceNumber(ServiceNumber);
    if (Service.IsValid()) {
      LDEBUG("ProcessNextService: Valid");
      LDEBUG("ProcessNextService: UpdateServiceNumber %d", ServiceNumber);
    }
    else {
      LWARNING("ProcessNextService: Invalid Service: %d", ServiceNumber);
    }
    if (ServiceNumber > StoredLength) {
      LWARNING("ProcessNextService Invalid service: ServiceNumber: %d",
          ServiceNumber);
    }
  }
  else {
    LWARNING("ProcessNextService: "
        "not enough room: ServiceNumber: %d - StoredLength: %d",
        ServiceNumber, StoredLength);
  }
#endif
#endif
}

uint8_t EIA708Packet::GetSequenceNumber() const
{
#ifndef DEBUG_VERBOSE
  return (HasData ? ((Data[HEADER_OFFSET] &
      SEQUENCE_MASK) >> SEQUENCE_BITS) : 0);
#else
  LINFO("GetSequenceNumber");
  if (!HasData) {
    LWARNING("GetSequenceNumber: Empty buffer");
    return 0;
  }
  uint8_t seq = ((Data[HEADER_OFFSET] & SEQUENCE_MASK) >> SEQUENCE_BITS);
  LDEBUG("GetSequenceNumber: data %x", Data[HEADER_OFFSET]);
  LDEBUG("GetSequenceNumber: %d", seq);
  return seq;
#endif
}

bool EIA708Packet::IsValid() const
{
#ifndef DEBUG_VERBOSE
  return (HasData && StoredLength == CompleteLength);
#else
  LINFO("IsValid");
  if (!HasData) {
    LWARNING("IsValid: Empty buffer");
    return false;
  }
  if (StoredLength == CompleteLength) {
    LDEBUG("IsValid: Yes (Complete package)");
    return true;
  }
  LWARNING("IsValid: No! (Incomplete package)");
  return false;
#endif
}

bool EIA708Packet::IsComplete() const
{
#ifndef DEBUG_VERBOSE
  return (StoredLength == CompleteLength);
#else
  LINFO("IsComplete");
  if (StoredLength == CompleteLength) {
    LDEBUG("IsComplete: Yes");
    return true;
  }
  LWARNING("IsComplete: No!");
  return false;
#endif
}

void EIA708Packet::SubstractLine21Info()
{
  CompleteLength -= 2;
#ifdef DEBUG_VERBOSE
  LINFO("SubstractLine21Info: substractied two bytes new "
      "CompleteLength: %d", (int)CompleteLength);
#endif
}

