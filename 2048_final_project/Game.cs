using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2048_final_project
{
    [Serializable]
    class Game
    {
        public int[,] value = new int[6, 6];
        public int score = new int();
        public int best = new int();
        public int blocks = new int();
        public bool die = false;
        public bool change = false;
        Random rd = new Random();

        public void Reset()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    value[i, j] = 0;
                }
            }
            blocks = 0;
            if (score > best)
                best = score;
            score = 0;
            die = false;
            change = false;
            Add();
            Add();
        }
        public void checkBlocks()
        {
            blocks = 0;
            for (int i = 1; i <= 4; i++)
            {
                for (int j = 1; j <= 4; j++)
                {
                    if (value[i, j] != 0)
                        blocks++;
                }
            }
        }
        public void Add()
        {
            int x = rd.Next(1, 5);
            int y = rd.Next(1, 5);
            if (value[x, y] == 0)
            {
                if (rd.Next(1, 101) >= 90)
                    value[x, y] = 4;
                else
                    value[x, y] = 2;
                blocks++;
                Die();
            }
            else
                Add();
        }
        public void Die()
        {
            checkBlocks();
            if (blocks == 16)
            {
                int count = 0;
                for (int i = 1; i <= 4; i++)
                {
                    int j = 1;
                    if (i % 2 == 0)
                        j = 2;
                    for (; j <= 4; j += 2)
                    {
                        if (!Neighbor(i, j))
                            count++;
                    }
                }
                if (count == 8)
                    die = true;
            }
        }
        public bool Neighbor(int x, int y)
        {
            //檢測左
            if (value[x, y] == value[x - 1, y])
                return true;
            //檢測右
            else if (value[x, y] == value[x + 1, y])
                return true;
            //檢測上
            else if (value[x, y] == value[x, y - 1])
                return true;
            //檢測下
            else if (value[x, y] == value[x, y + 1])
                return true;
            else
                return false;
        }


        //由於繪製方向問題x跟對調
        public void Up()
        {
            change = false;
            move_up();
            for (int i = 1; i <= 4; i++)
            {
                if (value[i, 1] == value[i, 2] && value[i, 1] + value[i, 2] != 0)
                {
                    value[i, 1] *= 2;
                    score += value[i, 1];
                    if (value[i, 3] == value[i, 4] && value[i, 3] + value[i, 4] != 0)
                    {
                        value[i, 2] = value[i, 3] * 2;
                        value[i, 3] = 0;
                        score += value[i, 2];
                    }
                    else
                    {
                        value[i, 2] = value[i, 3];
                        value[i, 3] = value[i, 4];
                    }
                    value[i, 4] = 0;
                }
                else if (value[i, 2] == value[i, 3] && value[i, 2] + value[i, 3] != 0)
                {
                    value[i, 2] *= 2;
                    value[i, 3] = value[i, 4];
                    value[i, 4] = 0;
                    score += value[i, 2];
                }
                else if (value[i, 3] == value[i, 4] && value[i, 3] + value[i, 4] != 0)
                {
                    value[i, 3] *= 2;
                    value[i, 4] = 0;
                    score += value[i, 3];
                }
            }
        }
        public void Down()
        {
            change = false;
            move_down();
            for (int i = 1; i <= 4; i++)
            {
                if (value[i, 4] == value[i, 3] && value[i, 4] + value[i, 3] != 0)
                {
                    value[i, 4] *= 2;
                    score += value[i, 4];
                    if (value[i, 2] == value[i, 1] && value[i, 2] + value[i, 1] != 0)
                    {
                        value[i, 3] = value[i, 2] * 2;
                        value[i, 2] = 0;
                        score += value[i, 3];
                    }
                    else
                    {
                        value[i, 3] = value[i, 2];
                        value[i, 2] = value[i, 1];
                    }
                    value[i, 1] = 0;
                }
                else if (value[i, 3] == value[i, 2] && value[i, 3] + value[i, 2] != 0)
                {
                    value[i, 3] *= 2;
                    value[i, 2] = value[i, 1];
                    value[i, 1] = 0;
                    score += value[i, 3];
                }
                else if (value[i, 2] == value[i, 1] && value[i, 2] + value[i, 1] != 0)
                {
                    value[i, 2] *= 2;
                    value[i, 1] = 0;
                    score += value[i, 2];
                }
            }
        }
        public void Left()
        {
            change = false;
            move_left();
            for (int j = 1; j <= 4; j++)
            {
                if (value[1, j] == value[2, j] && value[1, j] + value[2, j] != 0)
                {
                    value[1, j] *= 2;
                    score += value[1, j];
                    if (value[3, j] == value[4, j] && value[3, j] + value[4, j] != 0)
                    {
                        value[2, j] = value[3, j] * 2;
                        value[3, j] = 0;
                        score += value[2, j];
                    }
                    else
                    {
                        value[2, j] = value[3, j];
                        value[3, j] = value[4, j];
                    }   
                    value[4, j] = 0;
                }
                else if (value[2, j] == value[3, j] && value[2, j] + value[3, j] != 0)
                {
                    value[2, j] *= 2;
                    value[3, j] = value[4, j];
                    value[4, j] = 0;
                    score += value[2, j];
                }
                else if (value[3, j] == value[4, j] && value[3, j] + value[4, j] != 0)
                {
                    value[3, j] *= 2;
                    value[4, j] = 0;
                    score += value[3, j];
                }
            }
        }
        public void Right()
        {
            change = false;
            move_right();
            for (int j = 1; j <= 4; j++)
            {
                if (value[4, j] == value[3, j] && value[4, j] + value[3, j] != 0)
                {
                    value[4, j] *= 2;
                    score += value[4, j];
                    if (value[2, j] == value[1, j] && value[2, j] + value[1, j] != 0)
                    {
                        value[3, j] = value[2, j] * 2;
                        value[2, j] = 0;
                        score += value[3, j];
                    }
                    else
                    {
                        value[3, j] = value[2, j];
                        value[2, j] = value[1, j];
                    }
                    value[1, j] = 0;
                }
                else if (value[3, j] == value[2, j] && value[3, j] + value[2, j] != 0)
                {
                    value[3, j] *= 2;
                    value[2, j] = value[1, j];
                    value[1, j] = 0;
                    score += value[3, j];
                }
                else if (value[2, j] == value[1, j] && value[2, j] + value[1, j] != 0)
                {
                    value[2, j] *= 2;
                    value[1, j] = 0;
                    score += value[2, j];
                }
            }
        }

        public void move_up()
        {
            for (int i = 1; i <= 4; i++)
            {
                if (value[i, 1] == 0 && value[i, 2] + value[i, 3] + value[i, 4] != 0)
                {
                    value[i, 1] = value[i, 2];
                    value[i, 2] = value[i, 3];
                    value[i, 3] = value[i, 4];
                    value[i, 4] = 0;
                    change = true;
                    move_up();
                }
                else if (value[i, 2] == 0 && value[i, 3] + value[i, 4] != 0)
                {
                    value[i, 2] = value[i, 3];
                    value[i, 3] = value[i, 4];
                    value[i, 4] = 0;
                    change = true;
                    move_up();
                }
                else if (value[i, 3] == 0 && value[i, 4] != 0)
                {
                    value[i, 3] = value[i, 4];
                    value[i, 4] = 0;
                    change = true;
                }
            }
        }
        public void move_down()
        {
            for (int i = 1; i <= 4; i++)
            {
                if (value[i, 4] == 0 && value[i, 1] + value[i, 2] + value[i, 3] != 0)
                {
                    value[i, 4] = value[i, 3];
                    value[i, 3] = value[i, 2];
                    value[i, 2] = value[i, 1];
                    value[i, 1] = 0;
                    change = true;
                    move_down();
                }
                else if (value[i, 3] == 0 && value[i, 1] + value[i, 2] != 0)
                {
                    value[i, 3] = value[i, 2];
                    value[i, 2] = value[i, 1];
                    value[i, 1] = 0;
                    change = true;
                    move_down();
                }
                else if (value[i, 2] == 0 && value[i, 1] != 0)
                {
                    value[i, 2] = value[i, 1];
                    value[i, 1] = 0;
                    change = true;
                }
            }
        }
        public void move_left()
        {
            for (int j = 1; j <= 4; j++)
            {
                if (value[1, j] == 0 && value[2, j] + value[3, j] + value[4, j] != 0)
                {
                    value[1, j] = value[2, j];
                    value[2, j] = value[3, j];
                    value[3, j] = value[4, j];
                    value[4, j] = 0;
                    change = true;
                    move_left();
                }
                else if (value[2, j] == 0 && value[3, j] + value[4, j] != 0)
                {
                    value[2, j] = value[3, j];
                    value[3, j] = value[4, j];
                    value[4, j] = 0;
                    change = true;
                    move_left();
                }
                else if (value[3, j] == 0 && value[4, j] != 0)
                {
                    value[3, j] = value[4, j];
                    value[4, j] = 0;
                    change = true;
                }
            }
        }
        public void move_right()
        {
            for (int j = 1; j <= 4; j++)
            {
                if (value[4, j] == 0 && value[3, j] + value[2, j] + value[1, j] != 0)
                {
                    value[4, j] = value[3, j];
                    value[3, j] = value[2, j];
                    value[2, j] = value[1, j];
                    value[1, j] = 0;
                    change = true;
                    move_right();
                }
                else if (value[3, j] == 0 && value[2, j] + value[1, j] != 0)
                {
                    value[3, j] = value[2, j];
                    value[2, j] = value[1, j];
                    value[1, j] = 0;
                    change = true;
                    move_right();
                }
                else if (value[2, j] == 0 && value[1, j] != 0)
                {
                    value[2, j] = value[1, j];
                    value[1, j] = 0;
                    change = true;
                }
            }
        }
    }
}
