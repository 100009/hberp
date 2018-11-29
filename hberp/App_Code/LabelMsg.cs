using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace LXMS
{
    public class LableMsg
    {
        int roll_speed = 10; // 间隔(毫秒)
        int roll_step = 24; //(左滚) 步长
        int roll_topstep = 10; // （上滚）步长
        Font roll_font = new Font("黑体", 38, FontStyle.Bold);//字体
        Color roll_color = Color.Red;//字颜色
        int roll_left;//字距左边的距离
        int roll_top;//字距上方的距离
        Timer t_roll = new Timer();
        Size roll_TxtSize = Size.Empty;//字大小
        string roll_Text = null;//字内容
        Label labl = new Label();
        Image RollBackground = Image.FromFile(string.Format(@"{0}/Top.jpg", Application.StartupPath));
        string rolltype;//滚动方式
        Timer t_next = new Timer();//滚动间隔
        int next_time = 3000;//间隔时间
        string[] rollTxts;//滚动内容
        int roll_index = 0;//索引
        int ROLL_SPLIT = 10;//距下次距离

        Graphics GhF = null;
        public LableMsg(Label lb, string[] rollText, Graphics Gh, string rollType)
        {
            this.GhF = Gh;
            this.labl = lb;
            this.rolltype = rollType;
            this.rollTxts = rollText;
            switch (rolltype)
            {
                case "leftlong":
                    InitRoll(rollText[roll_index]);
                    break;
                case "right":
                    InitRoll(rollText[roll_index]);
                    break;
                case "top":
                    InitRoll(rollText[roll_index]);
                    break;
                case "leftstep":
                    InitLeftRoll(rollText);
                    break;
                default:
                    InitRoll(rollText[roll_index]);
                    break;
            }
        }

        private void InitLeftRoll(string[] content)
        {
            for (int i = 0; i < content.Length; i++)
            {
                roll_Text += content[i] + " ";
            }
            roll_left = RollBackground.Width;
            t_roll.Start();
            t_roll.Interval = 2;
            t_roll.Tick += new EventHandler(t_roll_Tick);
        }

        private void InitRoll(string rollText)
        {
            switch (rolltype)
            {
                case "leftlong":
                    roll_left = RollBackground.Width;
                    break;
                case "right":
                    roll_left = 0;
                    break;
            }
            roll_top = RollBackground.Height;
            roll_Text = rollText;
            //启动
            t_roll.Interval = roll_speed;
            t_roll.Tick += new EventHandler(t_roll_Tick);
            t_roll.Start();

            t_next.Interval = next_time;
            t_next.Tick += new EventHandler(t_next_Tick);
            t_next.Start();
        }

        void t_next_Tick(object sender, EventArgs e)
        {
            switch (rolltype)
            {
                case "leftlong":
                    this.roll_left = RollBackground.Width;
                    roll_index++;
                    if (roll_index == rollTxts.Count())
                    {
                        roll_index = 0;
                    }
                    this.roll_Text = this.rollTxts[roll_index];
                    break;
                case "leftstep":
                    this.roll_left = RollBackground.Width;
                    break;
                case "right":
                    this.roll_left = 0;
                    roll_index++;
                    if (roll_index == rollTxts.Count())
                    {
                        roll_index = 0;
                    }
                    this.roll_Text = this.rollTxts[roll_index];
                    break;
                case "top":
                    this.roll_top = RollBackground.Height;
                    roll_index++;
                    if (roll_index == rollTxts.Count())
                    {
                        roll_index = 0;
                    }
                    this.roll_Text = this.rollTxts[roll_index];
                    break;
            }
            this.t_roll.Start();
        }

        void t_roll_Tick(object sender, EventArgs e)
        {
            switch (rolltype)
            {
                case "top":
                    roll_top -= roll_topstep;//移动一个步长
                    if (roll_top <= 0)//如果移动到顶部
                    {
                        roll_top = 0;
                        this.DrawRoll();
                        t_roll.Stop();
                    }
                    break;
                case "leftlong":
                    roll_left -= roll_step;//移动一个步长
                    if (roll_left <= 0)//如果移动到最左侧
                    {
                        roll_left = 0;
                        this.DrawRoll();
                    }
                    break;
                case "leftstep":
                    roll_left -= 2;
                    Size proposedSize = new Size(int.MaxValue, int.MaxValue);

                    int roltext = TextRenderer.MeasureText(roll_Text, roll_font, proposedSize, TextFormatFlags.ModifyString).Width;//字符串长度
                    int nextroll = ROLL_SPLIT + roltext + RollBackground.Width;
                    if (roll_left <= (-TextRenderer.MeasureText(roll_Text, roll_font).Width - ROLL_SPLIT))
                    {
                        roll_left = 0;
                        this.DrawRoll();
                    }
                    break;
                case "right":
                    roll_left += roll_step;//移动一个步长
                    Size pSize = new Size(int.MaxValue, int.MaxValue);
                    if (roll_left >= Convert.ToInt32(RollBackground.Width - TextRenderer.MeasureText(roll_Text, roll_font, pSize, TextFormatFlags.ModifyString).Width * 0.75))//如果移动到最右侧
                    {
                        roll_left = Convert.ToInt32(RollBackground.Width - TextRenderer.MeasureText(roll_Text, roll_font, pSize, TextFormatFlags.ModifyString).Width * 0.75);
                        this.DrawRoll();
                        t_roll.Stop();
                    }
                    break;
            }
            this.DrawRoll();
        }
        private void DrawRoll()
        {
            Image bt = RollBackground.Clone() as Image;
            Graphics gh = Graphics.FromImage(bt);
            gh.Clear(Color.Black);
            switch (rolltype)
            {
                case "top":
                    gh.DrawString(roll_Text, roll_font, new SolidBrush(roll_color), 0, roll_top + 20);
                    break;
                case "leftlong":
                    gh.DrawString(roll_Text, roll_font, new SolidBrush(roll_color), roll_left, RollBackground.Height / 3);
                    break;
                case "leftstep":
                    gh.DrawString(roll_Text, roll_font, new SolidBrush(roll_color), roll_left, RollBackground.Height / 3);
                    gh.DrawString(roll_Text, roll_font, new SolidBrush(roll_color), roll_left + TextRenderer.MeasureText(roll_Text, roll_font).Width + ROLL_SPLIT, RollBackground.Height / 3);
                    break;
                case "right":
                    gh.DrawString(roll_Text, roll_font, new SolidBrush(roll_color), roll_left, RollBackground.Height / 3);
                    break;
            }
            DrawOnForm(bt, 0, 0, RollBackground.Width, RollBackground.Height);
            if (DateTime.Now.TimeOfDay.TotalMinutes % 5 == 0)
                GC.Collect();

        }
        protected void DrawOnForm(Image img, int x, int y, int width, int height)
        {
            GhF.DrawImage(img, x, y, width, height);
        }
    }
}
