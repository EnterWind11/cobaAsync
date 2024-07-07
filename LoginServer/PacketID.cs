using System;

namespace LoginServer
{
    public enum PacketID : ushort
    {
        LOGIN = 65535, //FFFF
        SERVER_SELECTION = 3301, //0CE5
        CHARA_SELECTION = 1701, //06A5
        CONFIRM = 1702, //06A6
    }
}