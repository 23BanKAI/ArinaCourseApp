using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArinaCourseApp
{
    internal class Alg
    {
        public void DoSmth(int[,] adjacencyMatrix)
        {
            // Читаем матрицу смежности
            int n = adjacencyMatrix.GetLength(0);

            // Преобразование матрицы смежности в матрицу инцидентности
            int[,] incidenceMatrix = new int[n, n * (n - 1) / 2];
            int index = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = i + 1; j < n; j++)
                {
                    if (adjacencyMatrix[i, j] == 1)
                    {
                        incidenceMatrix[i, index] = 1;
                        incidenceMatrix[j, index] = 1;
                        index++;
                    }
                }
            }

            // Вычисление ДНФ (дизъюнктивная нормальная форма) матрицы инцидентности
            List<List<int>> dnf = new List<List<int>>();
            for (int i = 0; i < n; i++)
            {
                List<int> clause = new List<int>();
                for (int j = 0; j < incidenceMatrix.GetLength(1); j++)
                {
                    if (incidenceMatrix[i, j] == 1)
                    {
                        clause.Add(j);
                    }
                }
                dnf.Add(clause);
            }

            // Вычисляем недостающие вершины для каждого слагаемого в ДНФ
            List<List<int>> missingVertices = new List<List<int>>();
            for (int i = 0; i < dnf.Count; i++)
            {
                List<int> term = new List<int>(dnf[i]);
                List<int> missing = new List<int>();
                for (int v = 0; v < n; v++)
                {
                    bool found = false;
                    for (int j = 0; j < term.Count; j++)
                    {
                        int e = term[j];
                        if (incidenceMatrix[v, e] == 1)
                        {
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        missing.Add(v);
                    }
                }
                missingVertices.Add(missing);
            }

            // Раскрашиваем вершины
            List<int> colors = new List<int>();
            for (int i = 0; i < missingVertices.Count; i++)
            {
                List<int> vertices = new List<int>(missingVertices[i]);
                for (int j = 0; j < colors.Count; j++)
                {
                    vertices.RemoveAll(v => colors.IndexOf(v) != -1);
                }
                if (vertices.Count > 0)
                {
                    colors.Add(vertices[0]);
                }
                else
                {
                    colors.Add(-1); // Следим за неокрашенными вершинами
                }
            }

            // Выводим цвета и количество используемых цветов
            string fileName = "test.txt";
            string textToWrite = "Colors:\n";
            for (int i = 0; i < colors.Count; i++)
            {
                textToWrite += ($"{i}: {(colors[i] == -1 ? "uncolored" : colors[i].ToString())}\n");
            }
            int numColors = colors.Max() + 1;
            textToWrite += ($"\nNumber of colors used: {numColors}");
            StreamWriter writer = new StreamWriter(fileName);
            writer.WriteLine(textToWrite);
            writer.Close();
        }
    }
}

