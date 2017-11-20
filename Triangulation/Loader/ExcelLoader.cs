using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Triangulation.Geometry;
using Triangulation.Zones;
using Excel = Microsoft.Office.Interop.Excel;

namespace Triangulation.Loader
{
    public class ExcelLoader : ILoader
    {
        private readonly Excel.Application _app;
        private Excel.Workbook _book;

        public ExcelLoader()
        {
            _app = new Excel.Application();
            _app.Visible = false;
            _app.ScreenUpdating = false;
        }

        public void Load(string filename, List<Vertex> vertices, Dictionary<int, ZoneInfo> zones)
        {
            Preconditions.CheckNotNull(zones, "zones");
            Preconditions.CheckNotNull(vertices, "vertices");
            Preconditions.CheckNotNullOrEmpty(filename, "filename");

            var id = 1; // id ерика
            var index = 2; // страница, с которой парсим ерики
            var offset = 14;
            var counter = 0;
            var mod = 3; // берем только каждую n-ую точку

            _book = _app.Workbooks.Open(filename, ReadOnly: true);

            while (true)
            {
                if (_book.Sheets.Count <= index) break;

                var sheet = _book.Sheets[index++];
                if (sheet == null) break;

                var rect = new Rectangle(3, 3, 11, 1500);
                var matrix = (object[,])sheet.UsedRange.Value2;

                while (true)
                {
                    if (matrix.GetLength(1) <= rect.X) break;

                    var value = matrix[rect.Y, rect.X]?.ToString();
                    if (string.IsNullOrWhiteSpace(value)) break;

                    var y = matrix[rect.Y, rect.X].ToString();
                    var x = matrix[rect.Y, rect.X + 1].ToString();

                    zones.Add(id, new ZoneInfo(id, 0, GetVertex(x, y, id)));

                    for (int i = 0; i < rect.Height; i++)
                    {
                        if (matrix.GetLength(0) <= rect.Y + i) break;

                        value = matrix[rect.Y + i, rect.X]?.ToString();
                        if (string.IsNullOrWhiteSpace(value)) break;

                        value = matrix[rect.Y + i, rect.X + 2].ToString();
                        if (value != "1") continue;

                        value = matrix[rect.Y + i, rect.X + 4] + matrix[rect.Y + i, rect.X + 5].ToString();
                        if (value != "10" && counter++ % mod != 0) continue;

                        y = matrix[rect.Y + i, rect.X].ToString();
                        x = matrix[rect.Y + i, rect.X + 1].ToString();

                        vertices.Add(GetVertex(x, y, id));
                    }

                    id++;
                    counter = 0;
                    rect.Offset(offset, 0);
                }

                ReleaseComObject(sheet);
            }

            Dispose();
        }

        private Vertex GetVertex(string x, string y, int id)
        {
            return new Vertex(Convert.ToInt32(x), 944 - Convert.ToInt32(y), id);
        }

        private void ReleaseComObject(object obj)
        {
            try
            {
                if (obj != null)
                {
                    Marshal.ReleaseComObject(obj);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(@"Unable to release the Object " + ex);
            }
            finally
            {
                GC.Collect();
            }
        }

        private void Dispose()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();

            if (_book != null)
            {
                _book.Close(false);
                Marshal.ReleaseComObject(_book);
            }

            if (_app != null)
            {
                _app.Quit();
                Marshal.ReleaseComObject(_app);
            }
        }
    }
}