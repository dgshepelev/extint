using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;

namespace AliensGame
{
    public class Player
    {
        public PictureBox img;
        /* stuff from params */
        private mainForm frm;
        private Thread thrd;
        private int fullHealth = 100;
        private int health;
        private int oldHealth;
        private int playerLives;
        private int movDistance;
        private int timerDelay;
        /* action vars */
        private bool halt = false;//for game pause
        private int state = 0; //0 dead, 1 active,2 exploding
        private int mov = 0; //0 stop,1 left,2 right
        private bool fire = false; //true to fire;
        /* position data */
        private int left;
        private int mid;
        private int right;
        private int top;
        private int bottom;
        private int width = 72;
        private int height = 40;
        /* internal stuff */
        private int fireLevel = 1;//missiles per shot
        private bool skinRefresh = false;//refresh skin when firelevel goes up
        private int animDelay = 0;//for explosion
        /* no idea but it won't work without it */
        delegate void setLocationCallBack(int top);
        //returns num of extra lives
        public int GetLives()
        {
            return playerLives;
        }
        public void SetLives(int modifier)
        {
            this.playerLives += modifier;
            frm.lblLives.Text = "x " + this.playerLives;
        }
        //kills thread
        public void Kill()
        {
            this.frm.Controls.Remove(img); 
            thrd.Abort();
        }
        public void Halt()
        {
            halt = true;
           this.img.Visible = false;
        }
        public void UnHalt()
        {
            halt = false;
            if (state > 0) this.img.Visible = true;
        }
		public int GetState() {
			return state;
		}
        public void SetState()
        {
            if (state == 2) state = 0;
            else state++;
        }
		public bool GetFire() {
			return fire;
		}
        public void SetFire(bool f)
        {
            fire = f;
        }
		public int GetMov() {
			return mov;
        }
        public void SetMov(int direction)
        {
            mov = direction;
        }
        public int GetLeft()
        {
            return left;
        }
        public int GetBottom()
        {
            return bottom;
        }
        public int GetRight()
        {
            return right;
        }
        public int GetMid()
        {
            return mid;
        }
        public int GetTop()
        {
            return top;
        }
		public int GetWidth() {
			return width;
		}
		public int GetHeight() {
			return height;
		}
		public void SetHealth(int modifier) {
            health += modifier;
            if (health > fullHealth) health = fullHealth;
            if (health < 0) health = 0;
            frm.lblHealth.Text = "Health: " + health + "/" + fullHealth;
        }
        public void UpFullHealth(int modifier)
        {
            fullHealth += modifier;
            frm.lblHealth.Text = "Health: " + health + "/" + fullHealth;
        }
        public void SetFireLevel(int val)
        {
            this.fireLevel = val;
            skinRefresh = true;
        }
        //initializes player data
        private void PlayerInit()
        {
            left = (frm.Width - GetWidth()) / 2;
            mid = GetLeft() + GetWidth() / 2;
            right = GetLeft() + GetWidth();
            this.img.Image = global::AliensGame.Properties.Resources.pl;
            this.img.Visible = true;
            this.img.SetBounds(left, top, width, height);
            SetHealth(fullHealth);
            frm.lblLives.Text = "x " + this.playerLives;
            frm.lblHealth.Text = "Health: " + health + "/" + fullHealth;
        }
        public Player(mainForm f, int timerDelay, int movDistance, int playerLives)
        {
            oldHealth = health = fullHealth;
            this.frm = f;
            this.timerDelay = timerDelay;
            this.movDistance = movDistance;
            this.playerLives = playerLives;
            this.img = new PictureBox();
            top = this.frm.Height - GetHeight() - 40;
            bottom = GetTop() + GetHeight();
            PlayerInit();
            this.frm.Controls.Add(img);
            this.thrd = new Thread(new ThreadStart(PlayerThreadStart));
            SetState();
            this.thrd.Start();
        }
        private void PlayerThreadStart()
        {
            while (true)
            {
                PlayerCycle(0);
                Thread.Sleep(timerDelay);
            }
        }
        private void PlayerCycle(int var)
        {
            if (frm.InvokeRequired)
            {
                setLocationCallBack d = new setLocationCallBack(PlayerCycle);
                frm.Invoke(d, new object[] { 5 });
            }
            else//the actual code
            {
                if ((state == 0 && playerLives == 0) || halt) return;
                if (state == 0 && playerLives > 0)//new player if dead with a life
                {
                    playerLives--;
                    PlayerInit();
                    SetState();
                    return;
                }
                if (state == 1)//move, update health and pic
                {
                    //move according to state
                    if (mov == 1 && img.Left - movDistance > 0)
                    {
                        img.Left -= movDistance;
                        left -= movDistance;
                        mid -= movDistance;
                        right -= movDistance;
                    }
                    if (mov == 2 && img.Left + img.Width + movDistance < frm.Width)
                    {
                        img.Left += movDistance;
                        left += movDistance;
                        mid += movDistance;
                        right += movDistance;
                    }

                    if (oldHealth != health||skinRefresh)//updates pic if health has changed
                    {
                        skinRefresh = false;
                        if (health >= fullHealth)
                        {
                            if (this.fireLevel == 1) img.Image = global::AliensGame.Properties.Resources.pl;
                            else if (this.fireLevel == 2) img.Image = global::AliensGame.Properties.Resources.p21;
                            else if (this.fireLevel == 3) img.Image = global::AliensGame.Properties.Resources.p31;
                        }
                        if (health < fullHealth) 
                        {
                            if (this.fireLevel == 1) img.Image = global::AliensGame.Properties.Resources.plb;
                            else if (this.fireLevel == 2) img.Image = global::AliensGame.Properties.Resources.p22;
                            else if (this.fireLevel == 3) img.Image = global::AliensGame.Properties.Resources.p32;
                        }
                        if (health < fullHealth / 2) 
                        {
                            if (this.fireLevel == 1) img.Image = global::AliensGame.Properties.Resources.plc;
                            else if (this.fireLevel == 2) img.Image = global::AliensGame.Properties.Resources.p23;
                            else if (this.fireLevel == 3) img.Image = global::AliensGame.Properties.Resources.p33;
                        }
                        if (health < fullHealth / 4) 
                        {
                            if (this.fireLevel == 1) img.Image = global::AliensGame.Properties.Resources.pld;
                            else if (this.fireLevel == 2) img.Image = global::AliensGame.Properties.Resources.p24;
                            else if (this.fireLevel == 3) img.Image = global::AliensGame.Properties.Resources.p34;
                        }
                        if (health <= 0)
                        {
                            img.Image = global::AliensGame.Properties.Resources.pld;
                            SetState();//will start dying next cycle
                        }
                        oldHealth = health;
                    }
                }
                else//explode
                {
                    if (animDelay==0) img.Image = global::AliensGame.Properties.Resources.pboom;
                    if (animDelay==30) img.Image = global::AliensGame.Properties.Resources.pboom1;
                    if (animDelay >= 60)
                    {
                        img.Visible = false;
                        state = 0;
                        animDelay = 0;
                    }
                    animDelay++;
                }
            }
        }
        //end of func
	}
    //end of class
}
