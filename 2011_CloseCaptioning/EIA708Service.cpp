#include <EIA708Service.h>

#define SYSID PCLCCPDS
#define CLASS "EIA708Service"
#define LOG_ENABLE 1
//#define LOG_DISABLE_TRACE 1
//#define LOG_DISABLE_DEBUG 1
//#define LOG_DISABLE_INFO 1
//#define LOG_DISABLE_WARNING 1
#include "debug.h"

#define DEBUG_VERBOSE 1

static const uint8_t FILLER = 0x00;
static const uint8_t OFFSET = 0x00;
static const uint8_t EXT_OFFSET = 0x01;
static const uint8_t DATA_OFFSET = 0x01;
static const uint8_t DATA_EXT_OFFSET = 0x02;
static const uint8_t MASK = 0xE0;
static const uint8_t EXT_MASK = 0x3F;
static const uint8_t BLOCK_SIZE_MASK = 0x1F;
static const uint8_t SERVICE_BITS = 5;
static const uint8_t EXT = 0x07;
static const uint8_t HEADER_LENGTH = 0x01;

void EIA708Service::Initialize(const uint8_t * data)
{
  HasData = false;
  uint8_t block = (Data[OFFSET] & BLOCK_SIZE_MASK) + HEADER_LENGTH;
  uint8_t number = (Data[OFFSET] & MASK) >> 5; 
  if (number > FILLER && number <= EXT)
  {
    if (number == EXT) {
        block += HEADER_LENGTH;
    }
    //memcpy(Data, data, block);
    for (uint8_t i = 0; i < block; i++) {
      Data[i] = data[i];
    }
    HasData = true;
  }
#if 0
#ifndef DEBUG_VERBOSE
  HasData = false;
  uint8_t number = GetServiceNumber(data);
  if (!IsValidService(number)) {
    if (number) {
      LWARNING("Initialize: service #%d Invalid! (exiting)", number);
    }
    return;
  }
  uint8_t block = GetPacketSize(data) + HEADER_LENGTH;
  if (number == EXT) {
    block += HEADER_LENGTH;
  }
  memcpy(Data, data, block);
  HasData = true;
#else
  LINFO("Initialize(data: %x %x %x)", data[0], data[1], data[2]);
  HasData = false;
  uint8_t number = GetServiceNumber(data);
  LDEBUG("Initialize: ServiceNumber: %d", number);
  if (!IsValidService(number)) {
    LWARNING("Initialize: Invalid! (exiting)");
    return;
  }
  uint8_t block = GetPacketSize(data) + HEADER_LENGTH;
  LDEBUG("Initialize: block size: %d", block);
  if (number == EXT) {
    block += HEADER_LENGTH;
  LDEBUG("Initialize: Adding HEADER_LENGTH - new block size: %d", block);
  }
  memcpy(Data, data, block);
  HasData = true;
#endif
#endif
}

bool EIA708Service::IsValid() const
{
#ifndef DEBUG_VERBOSE
  return (GetServiceNumber() > FILLER && GetServiceNumber() <= EXT);
#else
  LINFO("IsValid");
  uint8_t number = GetServiceNumber();
  LDEBUG("IsValid: GetServiceNumber(): %d", number);
  if (number > FILLER && number <= EXT) {
    LDEBUG("IsValid: Yes");
    return true;
  }
  LWARNING("IsValid: No!");
  return false;
#endif
}

uint8_t EIA708Service::GetExtensionNumber() const
{
#ifndef DEBUG_VERBOSE
  return (GetServiceNumber() == EXT ? (Data[EXT_OFFSET] & EXT_MASK) : 0);
#else
  LINFO("GetExtensionNumber");
  uint8_t number = GetServiceNumber();
  LDEBUG("GetExtensionNumber: GetServiceNumber(): %d", number);
  if (number == EXT) {
    LDEBUG("GetExtensionNumber: %d", (Data[EXT_OFFSET] & EXT_MASK));
    return (Data[EXT_OFFSET] & EXT_MASK);
  }
  LWARNING("GetExtensionNumber: No extension available!");
  return 0;
#endif
}

uint8_t EIA708Service::GetServiceNumber() const
{
#ifndef DEBUG_VERBOSE
  return (HasData ? ((Data[OFFSET] & MASK) >> SERVICE_BITS) : 0);
#else
  LINFO("GetServiceNumber");
  if (!HasData) {
    LWARNING("GetServiceNumber: Empty buffer (exiting)");
    return 0;
  }
  uint8_t number = (Data[OFFSET] & MASK) >> SERVICE_BITS;
  LDEBUG("GetServiceNumber: %d", number);
  return number;
#endif
}

uint8_t EIA708Service::GetPacketSize() const
{
#ifndef DEBUG_VERBOSE
  return (HasData ? (Data[OFFSET] & BLOCK_SIZE_MASK) : 0);
#else
  LINFO("GetPacketSize");
  if (!HasData) {
    LWARNING("GetPacketSize: Empty buffer (exiting)");
    return 0;
  }
  uint8_t number = (Data[OFFSET] & BLOCK_SIZE_MASK);
  LDEBUG("GetPacketSize: %d", number);
  return number;
#endif
}

const uint8_t * EIA708Service::GetCaptionData() const
{
#ifndef DEBUG_VERBOSE
  uint8_t service = GetServiceNumber();
  if (service == EXT) {
    return &Data[DATA_EXT_OFFSET];
  }
  else if (service > 0) {
    return &Data[DATA_OFFSET];
  }
  return 0;
#else
  LINFO("GetCaptionData");
  uint8_t service = GetServiceNumber();
  if (service == EXT) {
    LDEBUG("GetCaptionData: Extended data");
    return &Data[DATA_EXT_OFFSET];
  }
  else if (service > 0) {
    LDEBUG("GetCaptionData: Regular data");
    return &Data[DATA_OFFSET];
  }
  LWARNING("GetCaptionData: Invalid service number: %d", service);
  return 0;
#endif
}

uint8_t EIA708Service::UpdateServiceNumber(uint8_t number) const
{
#ifndef DEBUG_VERBOSE
  number += (Data[OFFSET] & BLOCK_SIZE_MASK);
  if (HasData && (((Data[OFFSET] & MASK) >> SERVICE_BITS) < EXT)) {
    number += HEADER_LENGTH;
  }
  else {
    number += (HEADER_LENGTH * 2);
  }
  return number;
#else
  LINFO("UpdateServiceNumber(number: %d)", number);
  number += (Data[OFFSET] & BLOCK_SIZE_MASK);
  LDEBUG("UpdateServiceNumber: After adding GetPacketSize(): number: %d",
      number);
  if (HasData && (((Data[OFFSET] & MASK) >> SERVICE_BITS) < EXT)) {
    number += HEADER_LENGTH;
  LDEBUG("UpdateServiceNumber: After adding HEADER_LENGTH(1): number: %d",
      number);
  }
  else {
    number += (HEADER_LENGTH * 2);
  LDEBUG("UpdateServiceNumber: After adding HEADER_LENGTH*2(2): number: %d",
      number);
  }
  LDEBUG("UpdateServiceNumber: Last: number: %d", number);
  return number;
#endif
}

uint8_t EIA708Service::GetServiceNumber(const uint8_t * data) const
{
#ifndef DEBUG_VERBOSE
  return ((data[OFFSET] & MASK) >> SERVICE_BITS);
#else
  LINFO("GetServiceNumber(data: %x)", data[0]);
  uint8_t number = (data[OFFSET] & MASK) >> SERVICE_BITS;
  LDEBUG("GetServiceNumber(data): %d", number);
  return number;
#endif
}

uint8_t EIA708Service::GetPacketSize(const uint8_t * data) const
{
#ifndef DEBUG_VERBOSE
  return (data[OFFSET] & BLOCK_SIZE_MASK);
#else
  LINFO("GetPacketSize(data: %x)", data[0]);
  uint8_t number = (data[OFFSET] & BLOCK_SIZE_MASK);
  LDEBUG("GetPacketSize(data): %d", number);
  return number;
#endif
}

bool EIA708Service::IsValidService(uint8_t service) const
{
#ifndef DEBUG_VERBOSE
  return (service > FILLER && service <= EXT);
#else
  LINFO("IsValidService(service: %x)", service);
  if (service > FILLER && service <= EXT) {
    LDEBUG("IsValidService: Is Valid");
    return true;
  }
  LWARNING("IsValidService: Invalid!");
  return false;
#endif
}

