#ifndef MOT_DTVCC_PACKET_H
#define MOT_DTVCC_PACKET_H

#include "EIA708Service.h"
#include <cstring>

static uint8_t DEFAULT_SERVICE = 1;

class EIA708Packet
{
public:
  EIA708Packet() :
    ServiceNumber(DEFAULT_SERVICE),
    CompleteLength(0),
    StoredLength(0),
    HasData(false),
    HasError(false)
  { memset(Data, 0, MAX_PACKET_SIZE); }
  ~EIA708Packet() {}

  void Zero() { Service.Zero(); memset(Data, 0, MAX_PACKET_SIZE);
    HasData = false; HasError = false; ServiceNumber = DEFAULT_SERVICE;
    CompleteLength = 0; StoredLength = 0;}
  void ZeroService() { Service.Zero(); }
  bool Initialize(const uint8_t * data);

  //Hack!
  void CompletePacket() { StoredLength = CompleteLength; HasError = true; }
  uint8_t GetMissingLength() { return CompleteLength - StoredLength; }
  bool IsInError() { return HasError; }
  void SubstractLine21Info();

  void ProcessNextService();
  uint8_t GetSequenceNumber() const;
  bool Store(const uint8_t * data);

  bool IsValid() const;
  bool IsComplete() const;
  inline uint8_t GetCompleteLength() const { return CompleteLength; }
  inline uint8_t GetStoredLength() const { return StoredLength; }

  inline bool HasValidService() const
  { return Service.IsValid(); }
  inline uint8_t GetServiceNumber() const
  { return Service.GetServiceNumber(); }
  inline uint8_t GetExtensionNumber() const
  { return Service.GetExtensionNumber(); }
  inline uint8_t GetPacketSize() const
  { return Service.GetPacketSize(); }
  inline const uint8_t* GetCaptionData() const
  { return Service.GetCaptionData(); }

private:
  uint8_t Data[MAX_PACKET_SIZE];
  EIA708Service Service;
  uint8_t ServiceNumber;
  uint8_t CompleteLength;
  uint8_t StoredLength;
  bool HasData;
  bool HasError;
};

#endif
