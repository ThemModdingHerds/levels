#pragma endian big

struct Pascal64String
{
    u64 length;
    char value[length];
};

Pascal64String version @ $;

struct Vector2
{
    float x;
    float y;
};

struct Vector3 : Vector2
{
    float z;
};

struct Vector4 : Vector3
{
    float w;
};

struct Bone
{
    Pascal64String name;
    u64 unknown1C;
    Vector3 unknown1s[unknown1C];
    u64 unknown2C;
    Vector4 unknown2s[unknown2C];
    u64 unknown3C;
    Vector3 unknown3s[unknown3C];
};

struct Unknown
{
    u8 data[4];
};

struct UVTrack
{
    Pascal64String name;
    u64 animationFramesC;
    Vector2 animationFrames[animationFramesC];
    u64 unknownC;
    Unknown unknowns[unknownC];
    u64 timeC;
    Vector2 time[timeC];
};

struct Header
{
    u32 unknown;
    u64 boneC;
    u64 trackC;
    float animationLengthInSeconds;
};

Header hds @ $;
Bone bones[hds.boneC] @ $;
UVTrack tracks[hds.trackC] @ $;