using System.IO;
using SharpCompress.Common;
using SharpCompress.Reader;
using SharpCompress.Writer;

namespace ComicBookNormalize.Common.IO
{
    public class Compressor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <param name="output"></param>
        public static void Decompress(string file, string output)
        {
            using (Stream stream = File.OpenRead(file))
            {
                var reader = ReaderFactory.Open(stream);
                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                        reader.WriteEntryToDirectory(output, ExtractOptions.ExtractFullPath | ExtractOptions.Overwrite);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="source"></param>
        /// <param name="file"></param>
        public static void Compress(string source, string file)
        {
            using (Stream stream = File.OpenWrite(file))
            {
                using (var writer = WriterFactory.Open(stream, ArchiveType.Zip, CompressionType.LZMA))
                {
                    writer.WriteAll(source, "*", SearchOption.AllDirectories);
                }
            }
        }
    }
}
