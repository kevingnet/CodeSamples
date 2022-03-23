#ifndef MOT_DTVCC_SERVICE_H
#define MOT_DTVCC_SERVICE_H

#include <cstring>
#include <stdint.h>

static const uint8_t MAX_PACKET_SIZE = 0x80;

class EIA708Service
{
public:
  EIA708Service() :
    HasData(false)
  { memset(Data, 0, MAX_PACKET_SIZE); }
  ~EIA708Service() {}

  void Initialize(const uint8_t * data);
  void Zero() { HasData = false; memset(Data, 0, MAX_PACKET_SIZE); }

  uint8_t GetExtensionNumber() const;
  uint8_t GetServiceNumber() const;
  uint8_t GetPacketSize() const;
  const uint8_t * GetCaptionData() const;
  bool IsValid() const;
  uint8_t UpdateServiceNumber(uint8_t number) const;

  uint8_t GetServiceNumber(const uint8_t * data) const;
  uint8_t GetPacketSize(const uint8_t * data) const;
  bool IsValidService(uint8_t service) const;
  bool HasSomeData() const { return HasData; }

private:
  uint8_t Data[MAX_PACKET_SIZE];
  bool HasData;
};

#endif
