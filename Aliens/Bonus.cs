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
    class Bonus
    {
        public PictureBox pic, spic;
        public bool state = true;
        public bool landed = false;
        public bool adding = false;
        public int type = 0;
        public int bonusFadeDelay;
        private int popCount = 0;
        private int lwidth = 252;
        private int lheight = 44;
        private int width = 56;
        private int height = 48;
        private int middle;
        private int sid; //object id
        private int sspd; //movement speed
        private bool halt = false;
        private Form sfrm;
        private Thread strd;
        private Random r;

        delegate void setLocationCallBack(int top);
        public void Halt()
        {
            halt = true;
            this.pic.Visible = false;
            this.spic.Visible = false;
        }
        public void UnHalt()
        {
            halt = false;
            if (this.state) this.pic.Visible = true;
            if (popCount > 0) this.spic.Visible = true;
        }
        public void Kill()
        {
            this.sfrm.Controls.Remove(pic);
            this.sfrm.Controls.Remove(spic);
            strd.Abort();
        }
        //called by level to let the bonus know it's been picked up
        public void Added()
        {
            bonusFadeDelay = 0;
            popCount = 55;
            adding = true;
            pic.Visible = false;
        }
        public void Stop()
        {
            state = false;
            pic.Visible = false;
            spic.Visible = false;
        }
        public Bonus(int id, int speed, int middle, int bottom, Form f, int bonusFadeDelay, Random r)
        {
            this.r=r;
            this.middle = middle;
            this.sfrm = f;
            this.sid = id;
            this.sspd = speed;
            this.bonusFadeDelay = bonusFadeDelay;
            this.type = GenType();
            pic = new PictureBox();
            pic.Image = global::AliensGame.Properties.Resources.bonus;
            pic.SizeMode = PictureBoxSizeMode.StretchImage;
            pic.SetBounds(middle - width/2, bottom - height, width, height);
            sfrm.Controls.Add(pic);
            spic = new PictureBox();
            spic.SizeMode = PictureBoxSizeMode.Zoom;
            spic.Visible = false;
            spic.SetBounds(middle - lwidth / 2, sfrm.Height - 100, lwidth, lheight);
            sfrm.Controls.Add(spic);
            SetDescPic(type);
            strd = new Thread(new ThreadStart(smov));
            strd.Start();
        }
        //selects the Nth description pic
        void SetDescPic(int n)
        {
            switch (n)
            {
                case 1:
                    spic.Image = global::AliensGame.Properties.Resources.bns100pts;
                    break;
                case 2:
                    spic.Image = global::AliensGame.Properties.Resources.bns1up;
                    break;
                case 3:
                    spic.Image = global::AliensGame.Properties.Resources.bnsfboost;
                    break;
                case 4:
                    spic.Image = global::AliensGame.Properties.Resources.bnshthbst;
                    break;
                case 5:
                    spic.Image = global::AliensGame.Properties.Resources.bnshthup;
                    break;
                case 6:
                    spic.Image = global::AliensGame.Properties.Resources.bnswup;
                    break;
                default:
                    break;
            }

        }
        int GenType()
        {
            int t = r.Next(1, 100);
            int n=2;//1up
            if (t > 5) n = 1;//100p
            if (t > 40) n = 4;//htbst
            if (t > 55) n = 5;//hthup
            if (t > 70) n = 3;//fboost
            if (t > 85) n = 6;//wup
            return n;
        }
        public void Init(int speed, int middle, int bottom, int bonusFadeDelay)
        {
            this.middle = middle;
            this.sspd = speed;
            this.bonusFadeDelay = bonusFadeDelay;
            pic.SetBounds(middle - width / 2, bottom - height, width, height);
            state = true;
            landed = false;
            pic.Visible = true;
            this.type = GenType();
            SetDescPic(this.type);
            spic.SetBounds(middle - lwidth / 2, sfrm.Height - 100 - lheight, lwidth, lheight);
            this.popCount = 0;
            this.adding = false;
        }
        private void smov()
        {
            while (true)
            {
                sstloc(this.sspd);
                Thread.Sleep(sspd);
            }
        }
        private void sstloc(int top)
        {
            if (sfrm.InvokeRequired)
            {
                setLocationCallBack d = new setLocationCallBack(sstloc);
                sfrm.Invoke(d, new object[] { top });
            }
            else
            {
                if (!state||halt) return;
                if (popCount > 0)//zooms out description
                {
                    if (popCount == 55) {
                        spic.SetBounds((spic.Location.X + spic.Width / 2) - spic.Width / 8, sfrm.Height - 100 - lheight / 4, spic.Width / 4, spic.Height / 4);
                        spic.Visible=true;
                    }
                    if (popCount == 50) {
                        spic.SetBounds((spic.Location.X + spic.Width / 2) - spic.Width / 2, sfrm.Height - 100 - lheight / 2, spic.Width * 2, spic.Height * 2);
                    }
                    if (popCount == 45) {
                        spic.SetBounds((spic.Location.X + spic.Width / 2) - ((spic.Width / 2) * 3) / 2, sfrm.Height - 100 - (lheight / 4) * 3, (spic.Width / 2) * 3, (spic.Height / 2) * 3);
                    }
                    popCount--;
                }
                if (!landed)
                {
                    pic.Top += top;
                    if (pic.Location.Y + pic.Height + top > this.sfrm.Height - 40)
                    {
                        landed = true;
                        pic.Top = this.sfrm.Height - 40 - pic.Height;
                    }
                }
                else//if landed counts down then disappears
                {
                    bonusFadeDelay--;
                    if (bonusFadeDelay <= 0&&popCount<=0) Stop();
                }
            }
        }
        //end of func
    }
    //end of class
}

