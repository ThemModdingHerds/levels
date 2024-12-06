#pragma endian big

struct Pascal64String
{
    u64 length;
    char value[length];
};

Pascal64String version @ $;
u64 count @ $;

struct Animation
{
    Pascal64String name;
    Pascal64String filename;
};

struct Element
{
    Pascal64String name;
    Pascal64String shape;
    float matrix[16];
    u8 visible;
    u8 unknown;
    u64 animationC;
    Animation animation[animationC];
};

Element elements[count] @ $;