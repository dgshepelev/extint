using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace AliensGame
{
    class Shot
    {
        public PictureBox pic;
        private Form sfrm;
        private Thread strd;
        private Random r;
        private bool state = true;
        private bool up;
        private int h = 0;
        private int sid;
        private int timerDelay;
        private int movDist;
        private bool halt = false;

        delegate void setLocationCallBack(int top);
        public bool GetState()
        {
            return this.state;
        }
        public void Halt()
        {
            this.halt = true;
            this.pic.Visible = false;
        }
        public void UnHalt()
        {
            this.halt = false;
            if (this.state) this.pic.Visible = true;
        }
        public void Kill()
        {
            this.sfrm.Controls.Remove(pic);
            this.strd.Abort();
        }
        public void Stop()
        {
            this.state = false;
            this.pic.Visible = false;
            this.h = 0;
        }
        public Shot(int id, int speed, int middle, int top, Form f, bool up, bool waitForInit, int h,Random r,int dist)
        {
            this.movDist = dist;
            this.r = r;
            this.h = h;
            this.sfrm = f;
            this.up = up;
            this.sid = id;
            this.timerDelay = speed;
            this.pic = new PictureBox();
            if (up) this.pic.Image = global::AliensGame.Properties.Resources.bs;
            else this.pic.Image = global::AliensGame.Properties.Resources.ms;
            this.pic.SizeMode = PictureBoxSizeMode.Zoom;
            this.pic.SetBounds(middle - 4, top, 8, 24);
            this.sfrm.Controls.Add(pic);
            this.strd = new Thread(new ThreadStart(smov));
            this.strd.Start();
            if (waitForInit) Stop();
        }
        public void Init(int speed, int middle, int top, int h,int dist)
        {
            this.h = h;
            this.timerDelay = speed;
            this.movDist = dist;
            this.pic.SetBounds(middle - 4, top, 8, 24);
            this.state = true;
            this.pic.Visible = true;
        }
        private void smov()
        {
            while (true)
            {
                sstloc(this.movDist);
                Thread.Sleep(timerDelay);
            }
        }
        private void sstloc(int movDist)
        {
            if (this.sfrm.InvokeRequired)
            {
                setLocationCallBack d = new setLocationCallBack(sstloc);
                sfrm.Invoke(d, new object[] { movDist });
            }
            else
            {
                if (this.halt||!this.state) return;
                if (this.up)
                {
                    this.pic.Top -= movDist;
                    if (this.h != 0) this.pic.Left += h;
                    if (this.pic.Location.Y < 50) Stop();
                }
                else
                {
                    this.pic.Top += movDist;
                    if (this.pic.Location.Y + this.pic.Height + movDist > this.sfrm.Height - 20) Stop();
                }
            }
        }
        //end of func
    }
    //end of class
}

