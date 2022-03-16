using SharpGL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace SharpGL
{
    public struct _RGBColor
    {
        public byte r;
        public byte g;
        public byte b;
    }

    public struct LinePoints
    {
        public int x1;
        public int y1;
        public int x2;
        public int y2;
    }
    public struct BoxPoints
    {
        public Point tl;
        public Point tr;
        public Point br;
        public Point bl;
    }
    public partial class fMain : Form
    {
        int n;
        Color colorUserColor;
        Color colorUserColor2;
        short shShape; //0: Line, 1:Circle

        Point pStart, pEnd;
        Point[] array = new Point[30];
        LinePoints[] lines = new LinePoints[20];
        int i;
        BoxPoints[] recs = new BoxPoints[20];
        int ib;
        BoxPoints[] cirs = new BoxPoints[20];
        int ic;
        BoxPoints[] elips = new BoxPoints[20];
        int ie;
        BoxPoints[] tris = new BoxPoints[20];
        int it;
        BoxPoints[] pens = new BoxPoints[20];
        int ip;
        bool flagMouseUp;
        bool flagMouseDown;
        bool flagDrawPolygon;
        bool flagDraw;
        bool flagBfill;
        bool flagSelect;
        bool flagMove;
        bool flagScale;
        int corner;
        
        
        int s;
        int sh;
        int size; //Độ dài của nét vẽ
        public fMain()
        {
            InitializeComponent();

            corner = -1;
            i = 0;
            ib = 0;
            ic = 0;
            ie = 0;
            it = 0;
            ip = 0;
            s = -1;
            sh = -1;
            colorUserColor = Color.Black;
            colorUserColor2 = Color.Black;
            n = 0;
            shShape = 0;
            flagDrawPolygon = false;
            flagMouseUp = false;
            flagMouseDown = false;
            flagBfill = false;
            flagSelect = false;
            flagMove = false;
            flagScale = false;

            size = 1;
        }

        private void openGLControl_OpenGLInitialized(object sender, EventArgs e)
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;

            //  Set the clear color.
            gl.ClearColor(255, 255, 255, 0);
            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            //  Load the identity.
            gl.LoadIdentity();

        }

        private void openGLControl_Resized(object sender, EventArgs e)
        {
            //  Get the OpenGL object.
            OpenGL gl = openGLControl.OpenGL;
            //  Create a perspective transformation.
            gl.Viewport(0, 0, openGLControl.Width, openGLControl.Height);

            //  Set the projection matrix.
            gl.MatrixMode(OpenGL.GL_PROJECTION);
            //  Load the identity.
            gl.LoadIdentity();
            gl.Ortho2D(0, openGLControl.Width, 0, openGLControl.Height);

        }
        private void openGLControl_OpenGLDraw_Line(OpenGL gl, int x1,int y1, int x2, int y2)
        {
            //StepX, StepY và các thông số liên quan
            int ix;
            int iy;
            int dx = x2 - x1;
            int dy = y2 - y1;

            //Gán bắt đầu ở (x1,y1)
            int x = x1;
            int y = y1;

            if (dx < 0)
            {
                dx = -dx;
                ix = -1;
            }
            else ix = 1;
            if (dy < 0)
            {
                dy = -dy;
                iy = -1;
            }
            else iy = 1;

            gl.PointSize(size);
            gl.Begin(OpenGL.GL_POINTS);
            // |m| <= 1
            if (dx >= dy)
            {

                int dx2 = 2 * dx;
                int dy2 = 2 * dy;
                int P = dy2 - dx;
                int dx2dy = dy2 - dx2;

                gl.Vertex(x, y);

                while (x != x2)
                {
                    if (P < 0)
                    {
                        x += ix;
                        P += dy2;
                    }
                    else if (P >= 0)
                    {
                        x += ix;
                        y += iy;
                        P += dx2dy;
                    }
                    gl.Vertex(x, y);

                }

            }
            //|m| > 1
            else if (dx < dy)
            {
                int dx2 = 2 * dx;
                int dy2 = 2 * dy;
                int P = dx2 - dy;
                int dx2dy = dx2 - dy2;

                gl.Vertex(x, y);
                while (y != y2)
                {
                    if (P < 0)
                    {
                        y += iy;
                        P += dx2;
                    }
                    else if (P >= 0)
                    {
                        x += ix;
                        y += iy;
                        P += dx2dy;
                    }
                    gl.Vertex(x, y);
                }
            }
            gl.End();
            gl.Flush();
        }

        private void openGLControl_OpenGLDraw_Circle(OpenGL gl, int x1, int y1, int x2, int y2)
        {
            //Tâm của hình tròn
            int xc = Math.Abs(x2 + x1) / 2;
            int yc = Math.Abs(y2 + y1) / 2;

            //Gán bắt đầu ở (0,r)
            int x = 0;
            int r = Math.Abs(y2 - y1) / 2; //Bán kính
            int y = r;

            gl.PointSize(size);
            gl.Begin(OpenGL.GL_POINTS);

            gl.Vertex(x + xc, y + yc);
            if (r > 0)
            {
                //Lấy đối xứng điểm
                gl.Vertex(x + xc, -y + yc);
                gl.Vertex(y + xc, x + yc);
                gl.Vertex(-y + xc, x + yc);
            }
            int P = 1 - r;
            while (x < y)
            {
                if (P < 0)
                {
                    x++;
                    P += 2 * x + 1;
                }
                else if (P >= 0)
                {
                    x++;
                    y--;
                    P += 2 * x - 2 * y + 1;
                }
                if (x > y)
                    break;
                //Lấy đối xứng điểm
                gl.Vertex(x + xc, y + yc);
                gl.Vertex(-x + xc, y + yc);
                gl.Vertex(x + xc, -y + yc);
                gl.Vertex(-x + xc, -y + yc);
                //Tránh trùng điểm
                if (x != y)
                {

                    gl.Vertex(y + xc, x + yc);
                    gl.Vertex(-y + xc, x + yc);
                    gl.Vertex(-y + xc, -x + yc);
                    gl.Vertex(y + xc, -x + yc);
                }
            }

            gl.End();
            gl.Flush();
        }
        private void openGLControl_OpenGLDraw_Rectangle(OpenGL gl, int x1, int y1, int x2, int y2)
        {
            //Vẽ cạnh từ (x1,y1) -> (x2,y1)
            openGLControl_OpenGLDraw_Line(gl, x1, y1, x2, y1);
            //Vẽ cạnh từ (x2,y1) -> (x2,y2)
            openGLControl_OpenGLDraw_Line(gl, x2, y1, x2, y2);
            //Vẽ cạnh từ (x2,y2) -> (x1,y2)
            openGLControl_OpenGLDraw_Line(gl, x2, y2, x1, y2);
            //Vẽ cạnh từ (x1,y2) -> (x1,y1)
            openGLControl_OpenGLDraw_Line(gl, x1, y2, x1, y1);
        }
        private void openGLControl_OpenGLDraw_Ellipse(OpenGL gl, int x1,int y1, int x2,int y2)
        {
            //Tâm của hình Ellipse
            int xc = Math.Abs(x2 + x1) / 2;
            int yc = Math.Abs(y2 + y1) / 2;

            int ry = Math.Abs(y2 - y1) / 2; //Bán trục ry
            int rx = Math.Abs(x2 - x1) / 2; //Bán trục rx
            //Gán bắt đầu ở (0,ry)
            int x = 0;
            int y = ry;

            //Tính thông số cơ bản
            int _2r2yx = 2 * ry * ry * x;
            int _2r2xy = 2 * rx * rx * y;
            int _r2x = rx * rx;
            int _r2y = ry * ry;
            double _p1 = _r2y - _r2x * ry + 0.25 * _r2x;

            gl.PointSize(size);
            gl.Begin(OpenGL.GL_POINTS);

            gl.Vertex(x + xc, y + yc);

            if (ry > 0)
            {
                gl.Vertex(x + xc, -y + yc);
            }
            //Lặp 1
            while (_2r2yx < _2r2xy)
            {
                if (_p1 < 0)
                {
                    x++;
                    _2r2yx = _2r2yx + 2 * _r2y;
                    _p1 = _p1 + _2r2yx + _r2y;
                }
                else if (_p1 >= 0)
                {
                    x++;
                    y--;
                    _2r2yx = _2r2yx + 2 * _r2y;
                    _2r2xy = _2r2xy - 2 * _r2x;
                    _p1 = _p1 + _2r2yx - _2r2xy + _r2y;
                }
                gl.Vertex(x + xc, y + yc);
                gl.Vertex(-x + xc, y + yc);
                gl.Vertex(x + xc, -y + yc);
                gl.Vertex(-x + xc, -y + yc);
            }
            //Hết lặp 1

            //Tính các thông số cơ bản
            _2r2xy = 2 * _r2x * y;
            _2r2yx = 2 * _r2y * x;
            double _p2 = _r2y * (x + 0.5) * (x + 0.5) + _r2x * (y - 1) * (y - 1) - _r2x*_r2y;

            gl.Vertex(x + xc, y + yc);
            gl.Vertex(-x + xc, y + yc);
            gl.Vertex(x + xc, -y + yc);
            gl.Vertex(-x + xc, -y + yc);
            //Lặp 2
            while (y != 0)
            {
                if (_p2 > 0)
                {
                    y--;
                    _2r2xy = _2r2xy - 2 * _r2x;
                    _p2 = _p2 - _2r2xy + _r2x;
                }
                else if (_p2 <= 0)
                {
                    x++;
                    y--;
                    _2r2yx = _2r2yx + 2 * _r2y;
                    _2r2xy = _2r2xy - 2 * _r2x;
                    _p2 = _p2 + _2r2yx - _2r2xy + _r2x;
                }
                gl.Vertex(x + xc, y + yc);
                gl.Vertex(-x + xc, y + yc);
                gl.Vertex(x + xc, -y + yc);
                gl.Vertex(-x + xc, -y + yc);
            }
            //Hết lặp 2

            gl.Vertex(rx + xc, yc);
            gl.Vertex(-rx + xc, yc);

            gl.End();
            gl.Flush();
        }

        private void openGLControl_OpenGLDraw_Triangle(OpenGL gl, int x1,int y1, int x2, int y2)
        {
            openGLControl_OpenGLDraw_Line(gl, x2, y1, x1, y1);
            openGLControl_OpenGLDraw_Line(gl, x2, y1, (x2+x1)/2, y2);
            openGLControl_OpenGLDraw_Line(gl, (x1+x2)/2, y2, x1, y1);

        }
        private void openGLControl_OpenGLDraw_Pentagon(OpenGL gl, int x1,int y1, int x2, int y2)
        {
            int R = (y2 - y1) / 2;

            int xc = (x2 + x1) / 2;
            int yc = (y2 + y1) / 2;



            int xdraw1, ydraw1;
            int xdraw2, ydraw2;

            int alpha = 90;
            while (alpha <= 378)
            {
                xdraw1 = (int)(R * (Math.Cos(alpha * 3.1415 / 180)));
                ydraw1 = (int)(R * (Math.Sin(alpha * 3.1415 / 180)));
                xdraw2 = (int)(R * (Math.Cos((alpha + 72) * 3.1415 / 180)));
                ydraw2 = (int)(R * (Math.Sin((alpha + 72) * 3.1415 / 180)));

                openGLControl_OpenGLDraw_Line(gl, xdraw1 + xc, ydraw1 + yc, xdraw2 + xc, ydraw2 + yc);
                alpha += 72;
            }
        }
        private void openGLControl_OpenGLDraw(object sender, SharpGL.RenderEventArgs args)
        {
            OpenGL gl = openGLControl.OpenGL;

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //Clear the color and depth buffer.
            //if (flagDraw)
            //{
            //    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
            //}

            // Vẽ vời chỗ này. Ví dụ:
            gl.Color(colorUserColor.R/255.0, colorUserColor.G/255.0, colorUserColor.B/255.0, 0); //Chọn màu theo colorUserColor
            //Đoạn thẳng
            if (flagDraw)
            {
                if (shShape == 0)
                {
                    //Khai báo 2 đỉnh đầu mút
                    int x1, y1, x2, y2;

                    x1 = pStart.X;
                    x2 = pEnd.X;
                    y1 = gl.RenderContextProvider.Height - pStart.Y;
                    y2 = gl.RenderContextProvider.Height - pEnd.Y;

                    openGLControl_OpenGLDraw_Line(gl, x1, y1, x2, y2);

                    lines[i].x1 = x1;
                    lines[i].x2 = x2;
                    lines[i].y1 = y1;
                    lines[i].y2 = y2;
                    i++;

                    flagDraw = false;
                }
                //Hình tròn
                else if (shShape == 1)
                {
                    int x1, y1, x2, y2;

                    x1 = pStart.X;
                    x2 = pEnd.X;
                    y1 = gl.RenderContextProvider.Height - pStart.Y;
                    y2 = gl.RenderContextProvider.Height - pEnd.Y;

                    openGLControl_OpenGLDraw_Circle(gl, x1, y1, x2, y2);
                    cirs[ib].tl.X = x1;
                    cirs[ib].tl.Y = y1;

                    cirs[ib].br.X = x2;
                    cirs[ib].br.Y = y2;

                    cirs[ib].tr.X = x2;
                    cirs[ib].tr.Y = y1;

                    cirs[ib].bl.X = x1;
                    cirs[ib].bl.Y = y2;
                    ic++;

                    flagDraw = false;
                }
                //Hình chữ nhật
                else if (shShape == 2)
                {
                    //Khai báo 2 đỉnh bất kỳ nằm trên đường chéo của hình chữ nhật
                    int x1, y1, x2, y2;

                    x1 = pStart.X;
                    x2 = pEnd.X;
                    y1 = gl.RenderContextProvider.Height - pStart.Y;
                    y2 = gl.RenderContextProvider.Height - pEnd.Y;

                    int tmp;

                    //Đổi (x1,y1) thành trái trên, (x2,y2) thành phải dưới
                    if (x2 < x1)
                    {
                        tmp = x1;
                        x1 = x2;
                        x2 = tmp;
                    }
                    if (y2 < y1)
                    {
                        tmp = y1;
                        y1 = y2;
                        y2 = tmp;
                    }
                    openGLControl_OpenGLDraw_Rectangle(gl, x1, y1, x2, y2);

                    recs[ib].tl.X = x1;
                    recs[ib].tl.Y = y1;

                    recs[ib].br.X = x2;
                    recs[ib].br.Y = y2;

                    recs[ib].tr.X = x2;
                    recs[ib].tr.Y = y1;

                    recs[ib].bl.X = x1;
                    recs[ib].bl.Y = y2;
                    ib++;
                    flagDraw = false;
                }
                //Hình Ellipse
                else if (shShape == 3)
                {
                    //Khai báo 2 đỉnh bất kỳ nằm trên đường chéo của hình chữ nhật ngoại tiếp hình Ellipse cần vẽ
                    int x1, y1, x2, y2;

                    x1 = pStart.X;
                    x2 = pEnd.X;
                    y1 = gl.RenderContextProvider.Height - pStart.Y;
                    y2 = gl.RenderContextProvider.Height - pEnd.Y;

                    openGLControl_OpenGLDraw_Ellipse(gl, x1, y1, x2, y2);
                    elips[ib].tl.X = x1;
                    elips[ib].tl.Y = y1;

                    elips[ib].br.X = x2;
                    elips[ib].br.Y = y2;

                    elips[ib].tr.X = x2;
                    elips[ib].tr.Y = y1;

                    elips[ib].bl.X = x1;
                    elips[ib].bl.Y = y2;
                    ie++;
                    flagDraw = false;
                }
                //Hình tam giác
                else if (shShape == 4)
                {
                    int x1, y1, x2, y2;

                    x1 = pStart.X;
                    x2 = pEnd.X;
                    y1 = gl.RenderContextProvider.Height - pStart.Y;
                    y2 = gl.RenderContextProvider.Height - pEnd.Y;
                    //Đổi (x1,y1) thành trái trên, (x2,y2) thành phải dưới
                    int tmp;
                    if (x2 < x1)
                    {
                        tmp = x1;
                        x1 = x2;
                        x2 = tmp;
                    }
                    if (y2 < y1)
                    {
                        tmp = y1;
                        y1 = y2;
                        y2 = tmp;
                    }

                    openGLControl_OpenGLDraw_Triangle(gl, x1, y1, x2, y2);
                    tris[ib].tl.X = x1;
                    tris[ib].tl.Y = y1;

                    tris[ib].br.X = x2;
                    tris[ib].br.Y = y2;

                    tris[ib].tr.X = x2;
                    tris[ib].tr.Y = y1;

                    tris[ib].bl.X = x1;
                    tris[ib].bl.Y = y2;
                    it++;
                    flagDraw = false;
                }
                //Hình ngũ giác
                else if (shShape == 5)
                {
                    int x1, y1, x2, y2;

                    x1 = pStart.X;
                    x2 = pEnd.X;
                    y1 = gl.RenderContextProvider.Height - pStart.Y;
                    y2 = gl.RenderContextProvider.Height - pEnd.Y;
                    //Đổi (x1,y1) thành trái trên, (x2,y2) thành phải dưới
                    int tmp;
                    if (x2 < x1)
                    {
                        tmp = x1;
                        x1 = x2;
                        x2 = tmp;
                    }
                    if (y2 < y1)
                    {
                        tmp = y1;
                        y1 = y2;
                        y2 = tmp;
                    }

                    openGLControl_OpenGLDraw_Pentagon(gl, x1, y1, x2, y2);
                    pens[ib].tl.X = x1;
                    pens[ib].tl.Y = y1;

                    pens[ib].br.X = x2;
                    pens[ib].br.Y = y2;

                    pens[ib].tr.X = x2;
                    pens[ib].tr.Y = y1;

                    pens[ib].bl.X = x1;
                    pens[ib].bl.Y = y2;
                    ip++;
                    flagDraw = false;

                }
                //Nếu shShape == 6 thì vẽ Polygon nối điểm cuối vào điểm đầu
                else if (shShape == 6)
                {

                    if (flagDrawPolygon)
                    {
                        if (n > 2)
                            openGLControl_OpenGLDraw_Line(gl, array[n-1].X, gl.RenderContextProvider.Height - array[n-1].Y, array[0].X, gl.RenderContextProvider.Height - array[0].Y);
                        n = 0;
                    }

                }
            }
            stopwatch.Stop();

            if(!flagMouseUp)
                tx_drawtime.Text = "Draw time: " + stopwatch.ElapsedTicks * 1.0 / 10000 + "ms"; //ms

        }

        private void openGLControl_Load(object sender, EventArgs e)
        {

        }

        private void fMain_Load(object sender, EventArgs e)
        {

        }

        private void bt_Color_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                colorUserColor = colorDialog1.Color;

            }
        }

        private void bt_Line_Click(object sender, EventArgs e)
        {
            shShape = 0;
            flagSelect = false;
            flagMove = false;
            flagDraw = false;
            flagScale = false;
        }

        private void bt_Circle_Click(object sender, EventArgs e)
        {
            shShape = 1;
            flagSelect = false;
            flagMove = false;
            flagDraw = false;
            flagScale = false;

        }

        private void bt_Rectangle_Click(object sender, EventArgs e)
        {
            shShape = 2;
            flagSelect = false;
            flagMove = false;
            flagScale = false;
            flagDraw = false;
        }

        private void bt_Ellipse_Click(object sender, EventArgs e)
        {
            shShape = 3;
            flagDraw = false;
            flagSelect = false;
            flagMove = false;
            flagScale = false;

        }


        private void bt_Triangle_Click(object sender, EventArgs e)
        {
            flagDraw = false;
            flagMove = false;
            flagScale = false;
            flagSelect = false;
            shShape = 4;
        }

        private void bt_Pentagon_Click(object sender, EventArgs e)
        {
            flagDraw = false;
            shShape = 5;
            flagSelect = false;
            flagScale = false;
            flagMove = false;

        }

        private void bt_Polygon_Click(object sender, EventArgs e)
        {
            flagDraw = false;
            flagMove = false;
            shShape = 6;
            flagScale = false;
            flagSelect = false;
        }
        private void bt_clear_Click(object sender, EventArgs e)
        {
            flagDraw = false;
            flagSelect = false;
            flagMove = false;
            flagScale = false;
            flagBfill = false;
            i = 0;
            ib = 0;
            //Neu bam nut Select ma pixel trang thi Circle
            OpenGL gl = openGLControl.OpenGL;

            gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

        }

        private void openGLControl_MouseDown(object sender, MouseEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            flagMouseUp = false;
            flagMouseDown = true;
            flagDraw = false;

            pStart = e.Location;
            pEnd = pStart;
            int narrow = 12;
            tx_coordstart.Text = "Start: (" + pStart.X + "; " + pStart.Y + ")";
            if (flagSelect)
            {
                for (int index = 0; index <= i; index++)
                {
                    if (lines[index].x1 <= pStart.X + narrow && lines[index].x1 >= pStart.X - narrow && lines[index].y1 <= gl.RenderContextProvider.Height - pStart.Y + narrow && lines[index].y1 >= gl.RenderContextProvider.Height - pStart.Y - narrow)
                    {
                        corner = 0;
                        s = index;
                        sh = 0;
                        break;
                    }
                     else if(lines[index].x2 <= pStart.X + narrow && lines[index].x2 >= pStart.X - narrow && lines[index].y2 <= gl.RenderContextProvider.Height - pStart.Y + narrow && lines[index].y2 >= gl.RenderContextProvider.Height - pStart.Y - narrow)
                    {
                        corner = 1;
                        s = index;
                        sh = 0;
                        break;
                    }
                }
                for (int index = 0; index <= ib; index++)
                {
                   if(pStart.X - narrow <= recs[index].tl.X && recs[index].tl.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= recs[index].tl.Y && recs[index].tl.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                   {
                        corner = 0;
                        s = index;
                        sh = 2;
                        break;
                    }
                   else if (pStart.X - narrow <= recs[index].tr.X && recs[index].tr.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= recs[index].tr.Y && recs[index].tr.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                   {
                        corner = 1;
                        s = index;
                        sh = 2;
                        break;
                   }
                   else if( pStart.X - narrow <= recs[index].br.X && recs[index].br.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= recs[index].br.Y && recs[index].br.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                   {
                        corner = 2;
                        s = index;
                        sh = 2;
                        break;
                   }
                   else if (pStart.X - narrow <= recs[index].bl.X && recs[index].bl.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= recs[index].bl.Y && recs[index].bl.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                   {
                        corner = 3;
                        s = index;
                        sh = 2;
                        break;
                   }
                }
                for (int index = 0; index <= ic; index++)
                {
                    if (pStart.X - narrow <= cirs[index].tl.X && cirs[index].tl.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= cirs[index].tl.Y && cirs[index].tl.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 0;
                        s = index;
                        sh = 1;
                        break;
                    }
                    else if (pStart.X - narrow <= cirs[index].tr.X && cirs[index].tr.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= cirs[index].tr.Y && cirs[index].tr.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 1;
                        s = index;
                        sh = 1;
                        break;
                    }
                    else if (pStart.X - narrow <= cirs[index].br.X && cirs[index].br.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= cirs[index].br.Y && cirs[index].br.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 2;
                        s = index;
                        sh = 1;
                        break;
                    }
                    else if (pStart.X - narrow <= cirs[index].bl.X && cirs[index].bl.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= cirs[index].bl.Y && cirs[index].bl.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 3;
                        s = index;
                        sh = 1;
                        break;
                    }
                }
                for (int index = 0; index <= ie; index++)
                {
                    if (pStart.X - narrow <= elips[index].tl.X && elips[index].tl.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= elips[index].tl.Y && elips[index].tl.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 0;
                        s = index;
                        sh = 3;
                        break;
                    }
                    else if (pStart.X - narrow <= elips[index].tr.X && elips[index].tr.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= elips[index].tr.Y && elips[index].tr.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 1;
                        s = index;
                        sh = 3;
                        break;
                    }
                    else if (pStart.X - narrow <= elips[index].br.X && elips[index].br.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= elips[index].br.Y && elips[index].br.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 2;
                        s = index;
                        sh = 3;
                        break;
                    }
                    else if (pStart.X - narrow <= elips[index].bl.X && elips[index].bl.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= elips[index].bl.Y && elips[index].bl.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 3;
                        s = index;
                        sh = 3;
                        break;
                    }
                }
                for (int index = 0; index <= it; index++)
                {
                    if (pStart.X - narrow <= tris[index].tl.X && tris[index].tl.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= tris[index].tl.Y && tris[index].tl.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 0;
                        s = index;
                        sh = 4;
                        break;
                    }
                    else if(pStart.X - narrow <= tris[index].tr.X && tris[index].tr.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= tris[index].tr.Y && tris[index].tr.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 1;
                        s = index;
                        sh = 4;
                        break;
                    }
                    else if (pStart.X - narrow <= tris[index].br.X && tris[index].br.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= tris[index].br.Y && tris[index].br.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 2;
                        s = index;
                        sh = 4;
                        break;
                    }
                    else if (pStart.X - narrow <= tris[index].bl.X && tris[index].bl.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= tris[index].bl.Y && tris[index].bl.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 3;
                        s = index;
                        sh = 4;
                        break;
                    }

                }
                for (int index = 0; index <= ip; index++)
                {
                    if (pStart.X - narrow <= pens[index].tl.X && pens[index].tl.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= pens[index].tl.Y && pens[index].tl.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 0;
                        s = index;
                        sh = 5;
                        break;
                    }
                    else if (pStart.X - narrow <= pens[index].tr.X && pens[index].tr.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= pens[index].tr.Y && pens[index].tr.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 1;
                        s = index;
                        sh = 5;
                        break;
                    }
                    else if (pStart.X - narrow <= pens[index].br.X && pens[index].br.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= pens[index].br.Y && pens[index].br.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 2;
                        s = index;
                        sh = 5;
                        break;
                    }
                    else if (pStart.X - narrow <= pens[index].bl.X && pens[index].bl.X <= pStart.X + narrow && gl.RenderContextProvider.Height - pStart.Y - narrow <= pens[index].bl.Y && pens[index].bl.Y <= gl.RenderContextProvider.Height - pStart.Y + narrow)
                    {
                        corner = 3;
                        s = index;
                        sh = 5;
                        break;
                    }
                }
                if (s != -1)
                {
                    if (sh == 0)
                    {
                        gl.PointSize(size + 5);
                        gl.Begin(OpenGL.GL_POINTS);

                        gl.Vertex(lines[s].x1, lines[s].y1);
                        gl.Vertex(lines[s].x2, lines[s].y2);

                        gl.End();
                        gl.Flush();
                    }
                    else if (sh == 1)
                    {
                        gl.PointSize(size + 5);
                        gl.Begin(OpenGL.GL_POINTS);

                        gl.Vertex(cirs[s].tl.X, cirs[s].tl.Y);
                        gl.Vertex(cirs[s].tr.X, cirs[s].tr.Y);
                        gl.Vertex(cirs[s].br.X, cirs[s].br.Y);
                        gl.Vertex(cirs[s].bl.X, cirs[s].bl.Y);

                        gl.End();
                        gl.Flush();
                    }
                    else if (sh == 2)
                    {
                        gl.PointSize(size + 5);
                        gl.Begin(OpenGL.GL_POINTS);

                        gl.Vertex(recs[s].tl.X, recs[s].tl.Y);
                        gl.Vertex(recs[s].tr.X, recs[s].tr.Y);
                        gl.Vertex(recs[s].br.X, recs[s].br.Y);
                        gl.Vertex(recs[s].bl.X, recs[s].bl.Y);

                        gl.End();
                        gl.Flush();
                    }
                    else if (sh == 3)
                    {
                        gl.PointSize(size + 5);
                        gl.Begin(OpenGL.GL_POINTS);

                        gl.Vertex(elips[s].tl.X, elips[s].tl.Y);
                        gl.Vertex(elips[s].tr.X, elips[s].tr.Y);
                        gl.Vertex(elips[s].br.X, elips[s].br.Y);
                        gl.Vertex(elips[s].bl.X, elips[s].bl.Y);

                        gl.End();
                        gl.Flush();
                    }
                    else if (sh == 4)
                    {
                        gl.PointSize(size + 5);
                        gl.Begin(OpenGL.GL_POINTS);

                        gl.Vertex(tris[s].tl.X, tris[s].tl.Y);
                        gl.Vertex(tris[s].tr.X, tris[s].tr.Y);
                        gl.Vertex(tris[s].br.X, tris[s].br.Y);
                        gl.Vertex(tris[s].bl.X, tris[s].bl.Y);

                        gl.End();
                        gl.Flush();
                    }
                    else if (sh == 5)
                    {
                        gl.PointSize(size + 5);
                        gl.Begin(OpenGL.GL_POINTS);

                        gl.Vertex(pens[s].tl.X, pens[s].tl.Y);
                        gl.Vertex(pens[s].tr.X, pens[s].tr.Y);
                        gl.Vertex(pens[s].br.X, pens[s].br.Y);
                        gl.Vertex(pens[s].bl.X, pens[s].bl.Y);

                        gl.End();
                        gl.Flush();
                    }
                }
            }
        }
        unsafe _RGBColor GetPixels(int x, int y)
        {
            

            OpenGL gl = openGLControl.OpenGL;
            var pixelData = new byte[3];
            gl.ReadPixels(x, y, 1, 1, OpenGL.GL_RGB, OpenGL.GL_UNSIGNED_BYTE, pixelData);
            _RGBColor color;
            color.r = pixelData[0];
            color.g = pixelData[1];
            color.b = pixelData[2];
            return color;
        }
        
        bool isSameColor(_RGBColor A, _RGBColor B)
        {
            return (A.r == B.r && A.g == B.g && A.b == B.b);
        }

        unsafe void PutPixel(int x, int y, _RGBColor F_Color)
        {
            OpenGL gl = openGLControl.OpenGL;
            byte[] pixel = new byte[3];
            pixel[0] = F_Color.r;
            pixel[1] = F_Color.g;
            pixel[2] = F_Color.b;

            gl.RasterPos(x, y);
            gl.DrawPixels(1, 1, OpenGL.GL_RGB, pixel);
            gl.Flush();
        }
        void BoundaryFill(int x, int y, _RGBColor F_Color, _RGBColor B_Color)
        {
            _RGBColor curColor;
            curColor = GetPixels(x, y);

            if (!isSameColor(curColor, B_Color) && !isSameColor(curColor, F_Color))
            {
                PutPixel(x, y, F_Color);
                if (x - 1 <= 0 || y - 1 <= 0 || x + 1 >= 300 || y + 1 >= 300)
                    return;
                BoundaryFill(x - 1, y, F_Color, B_Color);
                BoundaryFill(x, y+1, F_Color, B_Color);
                BoundaryFill(x+1, y, F_Color, B_Color);
                BoundaryFill(x, y-1, F_Color, B_Color);
            }
        }

        private void openGLControl_MouseMove(object sender, MouseEventArgs e)
        {
            OpenGL gl = openGLControl.OpenGL;
            if(e.Button == MouseButtons.Left) { 
            if (flagDraw)
            {
                if (!flagMouseUp)
                {
                    pEnd = e.Location;
                    tx_coordend.Text = "End: (" + pEnd.X + "; " + pEnd.Y + ")";
                    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT);
                }
                
            }
            }
        }
        private void openGLControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                flagDrawPolygon = false;
                flagMouseUp = true;
                flagMouseDown = false;
                pEnd = e.Location;
                tx_coordend.Text = "End: (" + pEnd.X + "; " + pEnd.Y + ")";
                OpenGL gl = openGLControl.OpenGL;
                //Vẽ 0->n-1 điểm trước của Polygon
                if (shShape == 6)
                {
                    array[n] = e.Location;
                    n++;
                    for(int i = 0; i< n -1; i++)
                    {
                        openGLControl_OpenGLDraw_Line(gl, array[i].X, gl.RenderContextProvider.Height - array[i].Y, array[i + 1].X, gl.RenderContextProvider.Height - array[i + 1].Y);
                    }
                }
                if(!flagSelect)
                    flagDraw = true;
                //Boundary Fill
                if (flagBfill)
                {

                    _RGBColor F_Color, B_Color;
                    F_Color.r = colorUserColor2.R;
                    F_Color.g = colorUserColor2.G;
                    F_Color.b = colorUserColor2.B;
                    B_Color.r = colorUserColor.R;
                    B_Color.g = colorUserColor.G;
                    B_Color.b = colorUserColor.B;
                    BoundaryFill(e.Location.X, gl.RenderContextProvider.Height - e.Location.Y, F_Color, B_Color);
                    flagBfill = false;
                }
            }
            else
            {
                flagDrawPolygon = true;
                flagDraw = true;
                
            }
            if (flagMove)
            {
                OpenGL gl = openGLControl.OpenGL;
                if (sh == 0)
                {

                    int d1 = pEnd.X - pStart.X;
                    int d2 = pEnd.Y - pStart.Y;
                    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                    lines[s].x1 += d1;
                    lines[s].y1 -= d2;
                    lines[s].x2 += d1;
                    lines[s].y2 -= d2;
                    openGLControl_OpenGLDraw_Line(gl, lines[s].x1, lines[s].y1, lines[s].x2, lines[s].y2);
                }
                else if (sh == 1)
                {
                    int d1 = pEnd.X - pStart.X;
                    int d2 = pEnd.Y - pStart.Y;
                    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                        cirs[s].tl.X += d1;
                        cirs[s].tl.Y -= d2;

                        cirs[s].tr.X += d1;
                        cirs[s].tr.Y -= d2;

                        cirs[s].br.X += d1;
                        cirs[s].br.Y -= d2;

                        cirs[s].bl.X += d1;
                        cirs[s].bl.Y -= d2;
                    openGLControl_OpenGLDraw_Circle(gl, cirs[s].tl.X, cirs[s].tl.Y, cirs[s].br.X, cirs[s].br.Y);
                }
                else if (sh == 2)
                {
                    int d1 = pEnd.X - pStart.X;
                    int d2 = pEnd.Y - pStart.Y;
                    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                        recs[s].tl.X += d1;
                        recs[s].tl.Y -= d2;

                        recs[s].tr.X += d1;
                        recs[s].tr.Y -= d2;

                        recs[s].br.X += d1;
                        recs[s].br.Y -= d2;

                        recs[s].bl.X += d1;
                        recs[s].bl.Y -= d2;

                    openGLControl_OpenGLDraw_Rectangle(gl, recs[s].tl.X, recs[s].tl.Y, recs[s].br.X, recs[s].br.Y);
                }
                else if (sh == 3)
                {
                    int d1 = pEnd.X - pStart.X;
                    int d2 = pEnd.Y - pStart.Y;
                    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                        elips[s].tl.X += d1;
                        elips[s].tl.Y -= d2;

                        elips[s].tr.X += d1;
                        elips[s].tr.Y -= d2;

                        elips[s].br.X += d1;
                        elips[s].br.Y -= d2;

                        elips[s].bl.X += d1;
                        elips[s].bl.Y -= d2;
                    openGLControl_OpenGLDraw_Ellipse(gl, elips[s].tl.X, elips[s].tl.Y, elips[s].br.X, elips[s].br.Y);
                }
                else if (sh == 4)
                {
                    int d1 = pEnd.X - pStart.X;
                    int d2 = pEnd.Y - pStart.Y;
                    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                        tris[s].tl.X += d1;
                        tris[s].tl.Y -= d2;

                        tris[s].tr.X += d1;
                        tris[s].tr.Y -= d2;

                        tris[s].br.X += d1;
                        tris[s].br.Y -= d2;

                        tris[s].bl.X += d1;
                        tris[s].bl.Y -= d2;
                    openGLControl_OpenGLDraw_Triangle(gl, tris[s].tl.X, tris[s].tl.Y, tris[s].br.X, tris[s].br.Y);
                }
                else if (sh == 5)
                {
                    int d1 = pEnd.X - pStart.X;
                    int d2 = pEnd.Y - pStart.Y;
                    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);

                        pens[s].tl.X += d1;
                        pens[s].tl.Y -= d2;

                        pens[s].tr.X += d1;
                        pens[s].tr.Y -= d2;

                        pens[s].br.X += d1;
                        pens[s].br.Y -= d2;

                        pens[s].bl.X += d1;
                        pens[s].bl.Y -= d2;
                    openGLControl_OpenGLDraw_Pentagon(gl, pens[s].tl.X, pens[s].tl.Y, pens[s].br.X, pens[s].br.Y);
                }
            }
            if (flagScale)
            {
                OpenGL gl = openGLControl.OpenGL;
                if (sh == 0)
                {
                    int d1 = pEnd.X - pStart.X;
                    int d2 = pEnd.Y - pStart.Y;
                    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                    if (corner == 0)
                    {
                        lines[s].x1 += d1;
                        lines[s].y1 -= d2;
                    }
                    else if (corner == 1)
                    {
                        lines[s].x2 += d1;
                        lines[s].y2 -= d2;
                    }
                    openGLControl_OpenGLDraw_Line(gl, lines[s].x1, lines[s].y1, lines[s].x2, lines[s].y2);
                }
                else if (sh == 1)
                {
                    int d1 = pEnd.X - pStart.X;
                    int d2 = pEnd.Y - pStart.Y;
                    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                    if (corner == 0)
                    {
                        cirs[s].tl.X += d1;
                        cirs[s].tl.Y -= d2;

                        cirs[s].bl.X += d1;
                        cirs[s].tr.Y -= d2;
                    }
                    else if (corner == 1)
                    {
                        cirs[s].tr.X += d1;
                        cirs[s].tr.Y -= d2;

                        cirs[s].tl.Y -= d2;
                        cirs[s].br.X += d1;
                    }
                    else if (corner == 2)
                    {
                        cirs[s].br.X += d1;
                        cirs[s].br.Y -= d2;

                        cirs[s].tr.X += d1;
                        cirs[s].bl.Y -= d2;
                    }
                    else if (corner == 3)
                    {
                        cirs[s].bl.X += d1;
                        cirs[s].bl.Y -= d2;

                        cirs[s].br.Y -= d2;
                        cirs[s].tl.X += d1;
                    }
                    openGLControl_OpenGLDraw_Circle(gl, cirs[s].tl.X, cirs[s].tl.Y, cirs[s].br.X, cirs[s].br.Y);
                }
                else if (sh == 2)
                {
                    int d1 = pEnd.X - pStart.X;
                    int d2 = pEnd.Y - pStart.Y;
                    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                    if (corner == 0)
                    {
                        recs[s].tl.X += d1;
                        recs[s].tl.Y -= d2;

                        recs[s].bl.X += d1;
                        recs[s].tr.Y -= d2;
                    }
                    else if (corner == 1)
                    {
                        recs[s].tr.X += d1;
                        recs[s].tr.Y -= d2;

                        recs[s].tl.Y -= d2;
                        recs[s].br.X += d1;
                    }
                    else if (corner == 2)
                    {
                        recs[s].br.X += d1;
                        recs[s].br.Y -= d2;

                        recs[s].tr.X += d1;
                        recs[s].bl.Y -= d2;
                    }
                    else if (corner == 3)
                    {
                        recs[s].bl.X += d1;
                        recs[s].bl.Y -= d2;

                        recs[s].br.Y -= d2;
                        recs[s].tl.X += d1;
                    }
                    openGLControl_OpenGLDraw_Rectangle(gl, recs[s].tl.X, recs[s].tl.Y, recs[s].br.X, recs[s].br.Y);
                }
                else if (sh == 3)
                {
                    int d1 = pEnd.X - pStart.X;
                    int d2 = pEnd.Y - pStart.Y;
                    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                    if (corner == 0)
                    {
                        elips[s].tl.X += d1;
                        elips[s].tl.Y -= d2;

                        elips[s].bl.X += d1;
                        elips[s].tr.Y -= d2;
                    }
                    else if (corner == 1)
                    {
                        elips[s].tr.X += d1;
                        elips[s].tr.Y -= d2;

                        elips[s].tl.Y -= d2;
                        elips[s].br.X += d1;
                    }
                    else if (corner == 2)
                    {
                        elips[s].br.X += d1;
                        elips[s].br.Y -= d2;

                        elips[s].tr.X += d1;
                        elips[s].bl.Y -= d2;
                    }
                    else if (corner == 3)
                    {
                        elips[s].bl.X += d1;
                        elips[s].bl.Y -= d2;

                        elips[s].br.Y -= d2;
                        elips[s].tl.X += d1;
                    }
                    openGLControl_OpenGLDraw_Ellipse(gl, elips[s].tl.X, elips[s].tl.Y, elips[s].br.X, elips[s].br.Y);
                }
                else if (sh == 4)
                {
                    int d1 = pEnd.X - pStart.X;
                    int d2 = pEnd.Y - pStart.Y;
                    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                    if (corner == 0)
                    {
                        tris[s].tl.X += d1;
                        tris[s].tl.Y -= d2;

                        tris[s].bl.X += d1;
                        tris[s].tr.Y -= d2;
                    }
                    else if (corner == 1)
                    {
                        tris[s].tr.X += d1;
                        tris[s].tr.Y -= d2;

                        tris[s].tl.Y -= d2;
                        tris[s].br.X += d1;
                    }
                    else if (corner == 2)
                    {
                        tris[s].br.X += d1;
                        tris[s].br.Y -= d2;

                        tris[s].tr.X += d1;
                        tris[s].bl.Y -= d2;
                    }
                    else if (corner == 3)
                    {
                        tris[s].bl.X += d1;
                        tris[s].bl.Y -= d2;

                        tris[s].br.Y -= d2;
                        tris[s].tl.X += d1;
                    }
                    openGLControl_OpenGLDraw_Triangle(gl, tris[s].tl.X, tris[s].tl.Y, tris[s].br.X, tris[s].br.Y);
                }
                else if (sh == 5)
                {
                    int d1 = pEnd.X - pStart.X;
                    int d2 = pEnd.Y - pStart.Y;
                    gl.Clear(OpenGL.GL_COLOR_BUFFER_BIT | OpenGL.GL_DEPTH_BUFFER_BIT);
                    if (corner == 0)
                    {
                        pens[s].tl.X += d1;
                        pens[s].tl.Y -= d2;

                        pens[s].bl.X += d1;
                        pens[s].tr.Y -= d2;
                    }
                    else if (corner == 1)
                    {
                        pens[s].tr.X += d1;
                        pens[s].tr.Y -= d2;

                        pens[s].tl.Y -= d2;
                        pens[s].br.X += d1;
                    }
                    else if (corner == 2)
                    {
                        pens[s].br.X += d1;
                        pens[s].br.Y -= d2;

                        pens[s].tr.X += d1;
                        pens[s].bl.Y -= d2;
                    }
                    else if (corner == 3)
                    {
                        pens[s].bl.X += d1;
                        pens[s].bl.Y -= d2;

                        pens[s].br.Y -= d2;
                        pens[s].tl.X += d1;
                    }
                    openGLControl_OpenGLDraw_Pentagon(gl, pens[s].tl.X, pens[s].tl.Y, pens[s].br.X, pens[s].br.Y);
                }
            }
        }
        private void cb_size_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox cb = sender as ComboBox;
            //Vì list item chỉ có 1px -> 8px nên lấy giá trị đầu tiên [0]
            char i = cb.SelectedItem.ToString()[0];
            //Đổi '0' -> '9' về int 
            size = Convert.ToInt32(i) - 48;
            //MessageBox.Show("Size la: " + size);
        }

        private void bt_FillColor_Click(object sender, EventArgs e)
        {
            if (colorDialog2.ShowDialog() == DialogResult.OK)
            {
                colorUserColor2 = colorDialog2.Color;

            }
        }

        private void bt_Select_Click(object sender, EventArgs e)
        {
            flagSelect = true;
            flagDraw = false;
            flagMove = false;
        }

        private void bt_Move_Click(object sender, EventArgs e)
        {
            flagMove = true;
            flagScale = false;
            flagBfill = false;
        }

        private void bt_Scale_Click(object sender, EventArgs e)
        {
            flagScale = true;
            flagMove = false;
            flagBfill = false;
        }

        private void bt_Bfill_Click(object sender, EventArgs e)
        {
            flagBfill = true;
            flagMove = false;
            flagScale = false;
        }

    }
}
