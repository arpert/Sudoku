
using System.IO;
using System;
using System.Collections;
using System.Collections.Generic;

public class Sudoku
{
   public static int[,] desk = new int[9, 9];
   public static int[,] flag = new int[9, 9];
   public static Random rnd = new Random();
   int x0 = 10, y0 = 10;
   int dx = 9, dy = 9;

   public void init(bool reset)
   {  
      if (reset) 
      {
         for(int iy = 0; iy < 9; iy++)
         {
            for(int ix = 0; ix < 9; ix++)
            {
               flag[ix, iy] = 0;
            }
         }
      }
      for(int sb = 0; sb < 9; sb++)
      {      
         init(sb);
      }
   }

   public void init(int sb)
   {  
      if (sb >= 0 && sb < 10)
      {
         ArrayList al = new ArrayList();
         for(int i = 0; i < 9; i++)
         {
            al.Add(i + 1);
         }

         for(int iy = 0; iy < 3; iy++)
         {
            for(int ix = 0; ix < 3; ix++)
            {
               int n = rnd.Next(al.Count);
//               Console.WriteLine("sb={0}, (x, y)=({1}, {2}), n={3}, numbers.Count = {4}, 3*(sb%3)+ix={5}, 3*(sb/3+iy)]", sb, ix, iy, n, al.Count, 3 * (sb % 3) + ix, 3 * (sb/3 + iy)]);

               desk[3 * (sb % 3) + ix, 3 * (sb/3) + iy] = (int)al[n];
               al.RemoveAt(n);
            }
         }
      } 
   }

/*   
      ┌─┬┐ ╔═╦╗ ╔═╤═══╗ ┌─╥───┐
      │ ││ ║ ║║ ║ │ ║ ║ │ ║ │ │
      ├─┼┤ ╠═╬╣ ╟─┼─╫─╢ ╞═╬═╪═╡
      └─┴┘ ╚═╩╝ ╚═╧═══╝ └─╨───┘
*/

   public void drawBoard()
   {
      for(int iy = 0; iy < dy; iy++)
      {
         for(int ix = 0; ix < dx; ix++)
         {
            Console.SetCursorPosition(x0 - 2 + 4 * ix, y0 - 1 + 2 * iy);  Console.Write("{1}{0}{0}{0}┼", iy % 3 == 0 ? "═" : "─", 
                    iy % 3 == 0 ? (ix % 3 == 0 ? "╬" : "╪") : (ix % 3 == 0 ? "╫" : "┼"));
            Console.SetCursorPosition(x0 - 2 + 4 * ix, y0     + 2 * iy);  Console.Write("{0}   {0}", ix % 3 == 0 ? "║" : "│");
            Console.SetCursorPosition(x0 - 2 + 4 * ix, y0 + 1 + 2 * iy);  Console.Write("┼{0}{0}{0}┼", iy % 3 == 0 ? "═" : "─");
         }
      }

      for(int ix = 0; ix < dx; ix++)
      {
         Console.SetCursorPosition(x0 - 2 + 4 * ix, y0 - 1 + 2 *  0); Console.Write("╦═══╦");
         Console.SetCursorPosition(x0 - 2 + 4 * ix, y0 - 1 + 2 * dy); Console.Write("╩═══╩");
      }

      for(int iy = 0; iy < dy; iy++)
      {
         Console.SetCursorPosition(x0 - 2 + 4 *  0, y0 - 1 + 2 * iy); Console.Write(iy % 3 == 0 ? "╠" : "╟");
         Console.SetCursorPosition(x0 - 2 + 4 *  0, y0 - 0 + 2 * iy); Console.Write("║");
         Console.SetCursorPosition(x0 - 2 + 4 * dx, y0 + 0 + 2 * iy); Console.Write("║");
         Console.SetCursorPosition(x0 - 2 + 4 * dx, y0 + 1 + 2 * iy); Console.Write(iy % 3 == 0 ? "╣" : "╢");
      }

      Console.SetCursorPosition(x0 - 2 + 4 *  0, y0 - 1 + 2 *  0); Console.Write("╔");
      Console.SetCursorPosition(x0 - 2 + 4 * dx, y0 - 1 + 2 *  0); Console.Write("╗");
      Console.SetCursorPosition(x0 - 2 + 4 *  0, y0 - 1 + 2 * dy); Console.Write("╚");
      Console.SetCursorPosition(x0 - 2 + 4 * dx, y0 - 1 + 2 * dy); Console.Write("╝");
   }
   

   public void show()
   {
      for(int iy = 0; iy < dy; iy++)
      {
         for(int ix = 0; ix < dx; ix++)
         {
            Console.SetCursorPosition(x0 -1 + 4 * ix, y0 + 2 * iy);
            Console.Write(" {0} ", desk[ix, iy]);
         }
      }
   }

   public int check()
   {
      int found = 0;
      for(int iy = 0; iy < dy; iy++)
      {
         HashSet<int> hs = new HashSet<int>();
         bool good = true;
         for(int ix = 0; ix < dx; ix++)
         {
            if (!hs.Add(desk[ix, iy]))
              good = false;
         }
         if (good)
         {
            found++;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            for(int ix = 0; ix < dx; ix++)
            {
               Console.SetCursorPosition(x0 + -1 + 4 * ix, y0 + 2 * iy); Console.Write(" {0} ", desk[ix, iy]);
               flag[ix, iy] = 1;
            }
            Console.BackgroundColor = ConsoleColor.Black;
         }
      }

      for(int ix = 0; ix < dx; ix++)
      {
         HashSet<int> hs = new HashSet<int>();
         bool good = true;
         for(int iy = 0; iy < dy; iy++)
         {
            if (!hs.Add(desk[ix, iy]))
              good = false;
         }
         if (good)
         {
            found++;
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            for(int iy = 0; iy < dy; iy++)
            {
               Console.SetCursorPosition(x0 + -1 + 4 * ix, y0 + 2 * iy); Console.Write(" {0} ", desk[ix, iy]);
            }
            Console.BackgroundColor = ConsoleColor.Black;
         }
      }

      return found;
   }


   public static void Main(string []argl)
   {
      Sudoku s = new Sudoku();
      Console.OutputEncoding = System.Text.Encoding.Unicode;
      Console.Clear();
      s.drawBoard();
      int found = 0;
      int n = 0;
      s.init(true);
      found = s.check();
      
      while (found < 3)
      {
         if (n != 0)
         s.init(n);
         s.show();
         found = s.check();
//         System.Threading.Thread.Sleep(900);
         n = (n + 1) % 9;
      }
   }
}