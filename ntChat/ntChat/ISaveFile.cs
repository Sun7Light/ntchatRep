using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace ntChat
{
    public interface ISaveFile
    {
        Stream GetSaveFileStream(FileMode fileMode);
    }
}
