using System;
using System.IO;

namespace Triangulation.Grd
{
    public class GrdFile
    {
        private FileInfo _fileInfo;

        public GrdFile()
        {
            Info = "DSBB";
            Row = 944;
            Column = 944;
            Data = new float[Row, Column];
            MinX = 8457525;
            MaxX = 8504675;
            MinY = 5364775;
            MaxY = 5411925;
        }

        private GrdFile(FileInfo fileInfo)
        {
            _fileInfo = fileInfo;
        }

        public string Info { get; set; }

        public short Row { get; set; }

        public short Column { get; set; }

        public float[,] Data { get; set; }

        public double MinX { get; set; }

        public double MaxX { get; set; }

        public double MinY { get; set; }

        public double MaxY { get; set; }

        public double MinZ { get; set; }

        public double MaxZ { get; set; }

        public void Write(string filename)
        {
            FileInfo fileInfo = new FileInfo(filename);

            using (var writer = new BinaryWriter(fileInfo.OpenWrite()))
            {
                writer.Write(Info.ToCharArray());

                writer.Write(Row);
                writer.Write(Column);

                writer.Write(MinX);
                writer.Write(MaxX);
                writer.Write(MinY);
                writer.Write(MaxY);
                writer.Write(MinZ);
                writer.Write(MaxZ);

                for (int i = Row - 1; i >= 0; i--)
                {
                    for (int j = 0; j < Column; j++)
                    {
                        writer.Write(Data[j, i]);
                    }
                }
            }
        }

        [Obsolete("NewInstance is deprecated, use Read() instead")]
        public static GrdFile NewInstance()
        {
            GrdFile grd = new GrdFile();

            return grd;
        }

        public static GrdFile Read(FileInfo fileInfo)
        {
            var grd = new GrdFile(fileInfo);

            using (var reader = new BinaryReader(fileInfo.OpenRead()))
            {
                grd.Info = new string(reader.ReadChars(4));

                grd.Row = reader.ReadInt16();
                grd.Column = reader.ReadInt16();

                grd.MinX = reader.ReadDouble();
                grd.MaxX = reader.ReadDouble();
                grd.MinY = reader.ReadDouble();
                grd.MaxY = reader.ReadDouble();
                grd.MinZ = reader.ReadDouble();
                grd.MaxZ = reader.ReadDouble();

                grd.Data = new float[grd.Row, grd.Column];
                for (int i = grd.Row - 1; i >= 0; i--)
                {
                    for (int j = 0; j < grd.Column; j++)
                    {
                        grd.Data[j, i] = reader.ReadSingle();
                    }
                }
            }

            return grd;
        }

        public static GrdFile Read(string filename)
        {
            return Read(new FileInfo(filename));
        }
    }
}