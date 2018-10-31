using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ITCLibrary
{
    public class Canada
    {
        public struct CellPos
        {
            public int X;
            public int Y;
        }

        private static Dictionary<PictureBox, Cell> Pics = new Dictionary<PictureBox, Cell>();
        private static Dictionary<Cell, Cell.CellType> CellTypes = new Dictionary<Cell, Cell.CellType>();
        private static PictureBox ClickedPB;
        private static List<PictureBox> ChosenPics = new List<PictureBox>();

        public static void Ini(Form form)
        {
            form.BackColor = Color.Red;
            Bitmap image = (Bitmap)Bitmap.FromFile("Canada.png");
            for (int x = 0; x < 20; x++)
            {
                for (int y = 0; y < 20; y++)
                {
                    Bitmap im = image.Clone(new Rectangle(x * 50 + 1, y * 50 + 1, 49, 49), image.PixelFormat);
                    Cell c = new Cell(im, x, y);
                    CellTypes[c] = c.CType;
                    if (c.CType != Cell.CellType.Inner)
                    {
                        Graphics.FromImage(im).DrawRectangle(new Pen(Color.Blue, 3), 20, 20, 9, 9);
                    }
                    PictureBox p = new PictureBox
                    {
                        Name = string.Format("pictureBox_{0}_{1}", x, y),
                        Size = new Size(25, 25),
                        Location = new Point(x * 26, y * 26),
                        Image = im,
                        SizeMode = PictureBoxSizeMode.StretchImage
                    };
                    Pics.Add(p, c);
                    RestoreBorderStyle(p);
                    form.Controls.Add(p);
                    p.MouseClick += new MouseEventHandler(PictureBoxClicked);
                }
            }
            Dictionary<Cell.CellType, List<Cell>> cTypesCount = new Dictionary<Cell.CellType, List<Cell>>();
            foreach (Cell c in CellTypes.Keys)
            {
                if (cTypesCount.ContainsKey(CellTypes[c]))
                {
                    cTypesCount[CellTypes[c]].Add(c);
                }
                else
                {
                    cTypesCount.Add(CellTypes[c], new List<Cell>() { c });
                }
            }
        }

        private static void SwapPositions(PictureBox p1, PictureBox p2)
        {
            Point l1 = p1.Location;
            Point l2 = p2.Location;
            CellPos cp1 = Pics[p1].Position;
            CellPos cp2 = Pics[p2].Position;
            Pics[p1].Position = cp2;
            Pics[p2].Position = cp1;
            p1.Location = l2;
            p2.Location = l1;
        }

        private static void RestoreBorderStyle(PictureBox p)
        {
            if (CellTypes[Pics[p]] == Cell.CellType.Inner)
            {
                p.BorderStyle = BorderStyle.None;
            }
            else
            {
                p.BorderStyle = BorderStyle.FixedSingle;
            }
        }

        private void SaveStatus()
        {
            StringBuilder sb = new StringBuilder();
            foreach (PictureBox picB in Pics.Keys)
            {
                sb.AppendLine(string.Format("{0} X:{1} Y:{2}", picB.Name, picB.Left / 26, picB.Top / 26));
            }
            System.IO.File.WriteAllText("Status.txt", sb.ToString());
        }

        private static void PictureBoxClicked(object sender, MouseEventArgs e)
        {
            PictureBox pb = (PictureBox)sender;
            if (e.Button == MouseButtons.Left)
            {
                if (ClickedPB == null)
                {
                    ClickedPB = pb;
                    ClickedPB.BorderStyle = BorderStyle.Fixed3D;
                }
                else if (ClickedPB == pb)
                {
                    RestoreBorderStyle(ClickedPB);
                    ClickedPB = null;
                }
                else
                {
                    SwapPositions(ClickedPB, pb);
                    RestoreBorderStyle(ClickedPB);
                    ClickedPB = null;
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                if (ClickedPB != null)
                {
                    RestoreBorderStyle(ClickedPB);
                    ClickedPB = null;
                }
                foreach (PictureBox cp in ChosenPics)
                {
                    RestoreBorderStyle(cp);
                }
                ChosenPics.Clear();
                Cell cell = Pics[pb];
                foreach (PictureBox p in Pics.Keys)
                {
                    Cell c = Pics[p];
                    if (
                        (cell.LB == c.RB && cell.LT == c.RT) ||
                        (cell.RB == c.LB && cell.RT == c.LT) ||
                        (cell.LB == c.LT && cell.RB == c.RT) ||
                        (cell.LT == c.LB && cell.RT == c.RB)
                        )
                    {
                        ChosenPics.Add(p);
                        p.BorderStyle = BorderStyle.Fixed3D;
                    }
                }
            }
            else if (e.Button == MouseButtons.Middle)
            {
                if (ClickedPB != null)
                {
                    RestoreBorderStyle(ClickedPB);
                    ClickedPB = null;
                }
                foreach (PictureBox cp in ChosenPics)
                {
                    RestoreBorderStyle(cp);
                }
                ChosenPics.Clear();
            }
        }

        public class Cell
        {
            public enum CellType
            {
                Inner,
                LBorder,
                RBorder,
                BBorder,
                TBorder,
                LBCorner,
                LTCorner,
                RBCorner,
                RTCorner
            }

            public static readonly Color BLACK = Color.FromArgb(255, 0, 0, 0);
            public static readonly Color WHITE = Color.FromArgb(255, 255, 255, 255);

            public Cell(Bitmap im, int x, int y)
            {
                Content = im;
                Codes = new Color[2, 2];
                Codes[0, 0] = GetPixColor(0, 0);
                Codes[0, 1] = GetPixColor(0, 48);
                Codes[1, 0] = GetPixColor(48, 0);
                Codes[1, 1] = GetPixColor(48, 48);
            }

            private Color GetPixColor(int x, int y)
            {
                Color c = Content.GetPixel(x, y);
                if (EmptyC(c))
                {
                    c = BLACK;
                }
                return c;
            }

            public bool EmptyC(Color c)
            {
                return c == BLACK || c == WHITE;
            }

            public bool FilledC(Color c)
            {
                return !EmptyC(c);
            }

            public Color LB { get { return Codes[0, 0]; } }
            public Color LT { get { return Codes[0, 1]; } }
            public Color RB { get { return Codes[1, 0]; } }
            public Color RT { get { return Codes[1, 1]; } }

            public int X { get { return Position.X; } set { Position.X = value; } }
            public int Y { get { return Position.Y; } set { Position.Y = value; } }

            public Color[,] Codes;

            public Bitmap Content;

            public CellPos Position;

            public bool IsInnerCell { get { return FilledC(LB) && FilledC(LT) && FilledC(RB) && FilledC(RT); } }
            public bool IsLBorder { get { return EmptyC(LB) && EmptyC(LT) && FilledC(RB) && FilledC(RT); } }
            public bool IsRBorder { get { return FilledC(LB) && FilledC(LT) && EmptyC(RB) && EmptyC(RT); } }
            public bool IsBBorder { get { return EmptyC(LB) && FilledC(LT) && EmptyC(RB) && FilledC(RT); } }
            public bool IsTBorder { get { return FilledC(LB) && EmptyC(LT) && FilledC(RB) && EmptyC(RT); } }
            public bool IsLBCorner { get { return EmptyC(LB) && EmptyC(LT) && EmptyC(RB) && FilledC(RT); } }
            public bool IsLTCorner { get { return EmptyC(LB) && EmptyC(LT) && FilledC(RB) && EmptyC(RT); } }
            public bool IsRBCorner { get { return EmptyC(LB) && FilledC(LT) && EmptyC(RB) && EmptyC(RT); } }
            public bool IsRTCorner { get { return FilledC(LB) && EmptyC(LT) && EmptyC(RB) && EmptyC(RT); } }

            private CellType _CTypeCache;
            private bool _CTypeCached = false;

            public CellType CType
            {
                get
                {
                    if (!_CTypeCached)
                    {
                        _CTypeCache =
                            IsInnerCell ? CellType.Inner :
                            IsLBorder ? CellType.LBorder :
                            IsRBorder ? CellType.RBorder :
                            IsBBorder ? CellType.BBorder :
                            IsTBorder ? CellType.TBorder :
                            IsLBCorner ? CellType.LBCorner :
                            IsLTCorner ? CellType.LTCorner :
                            IsRBCorner ? CellType.RBCorner :
                            IsRTCorner ? CellType.RTCorner :
                            throw new Exception();
                        _CTypeCached = true;
                    }
                    return _CTypeCache;
                }
            }
        }
    }
}
