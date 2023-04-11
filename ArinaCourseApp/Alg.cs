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
        private int vertices;
        private int[,] adjacencyMatrix;
        public Alg(int v, int[,] adjMatrix)
        {
            vertices = v;
            adjacencyMatrix = adjMatrix;
        }
        // функция для получения матрицы инцидентности из матрицы смежности
        public int[,] GetIncidenceMatrix()
        {
            int[,] incidenceMatrix = new int[vertices, vertices * (vertices - 1) / 2];
            int k = 0;

            for (int i = 0; i < vertices; i++)
            {
                for (int j = i + 1; j < vertices; j++)
                {
                    if (adjacencyMatrix[i, j] == 1)
                    {
                        incidenceMatrix[i, k] = 1;
                        incidenceMatrix[j, k] = 1;
                        k++;
                    }
                }
            }

            return incidenceMatrix;
        }

        // функция для получения дизъюнктивной нормальной формы (ДНФ) из матрицы инцидентности
        public List<HashSet<int>> GetDNF(int[,] incidenceMatrix)
        {
            List<HashSet<int>> dnf = new List<HashSet<int>>();

            for (int i = 0; i < incidenceMatrix.GetLength(1); i++)
            {
                HashSet<int> term = new HashSet<int>();
                for (int j = 0; j < vertices; j++)
                {
                    if (incidenceMatrix[j, i] == 1)
                    {
                        term.Add(j);
                    }
                }
                dnf.Add(term);
            }

            return dnf;
        }

        // функция для записи недостающих вершин для каждого терма в ДНФ
        public void WriteMissingVertices(ref List<HashSet<int>> dnf)
        {
            HashSet<int> complete = new HashSet<int>();
            for (int i = 0; i < vertices; i++)
            {
                complete.Add(i);
            }

            for (int i = 0; i < dnf.Count; i++)
            {
                HashSet<int> missingVertices = new HashSet<int>(complete);
                foreach (int v in dnf[i])
                {
                    missingVertices.Remove(v);
                }
                dnf[i].UnionWith(missingVertices);
            }
        }

        // функция для раскрашивания графа с использованием результирующих наборов отсутствующих вершин
        public int ColorGraph(List<HashSet<int>> missingVertices)
        {
            int[] colors = new int[vertices];
            int colorCount = 1;

            foreach (HashSet<int> set in missingVertices)
            {
                foreach (int v in set)
                {
                    HashSet<int> adjacentNodes = new HashSet<int>();
                    for (int i = 0; i < vertices; i++)
                    {
                        if (adjacencyMatrix[v, i] == 1)
                        {
                            adjacentNodes.Add(i);
                        }
                    }

                    HashSet<int> usedColors = new HashSet<int>();
                    foreach (int adj in adjacentNodes)
                    {
                        if (colors[adj] != 0)
                        {
                            usedColors.Add(colors[adj]);
                        }
                    }

                    int newColor = 1;
                    while (usedColors.Contains(newColor))
                    {
                        newColor++;
                    }

                    colors[v] = newColor;
                    if (newColor > colorCount)
                    {
                        colorCount = newColor;
                    }
                }
            }

            // Выводим цвета и количество используемых цветов
            string fileName = "test.txt";
            string textToWrite = "Vertex\tColor\n";
            for (int i = 0; i < vertices; i++)
            {
                textToWrite += (i + "\t" + colors[i] + "\n");
            }
            textToWrite += ("\nNumber of colors used: " + colorCount);
            StreamWriter writer = new StreamWriter(fileName);
            writer.WriteLine(textToWrite);
            writer.Close();

            return colorCount;
        }

        public void DoSmth(int[,] adjacencyMatrix)
        {
            int vertices = 9;

            Alg g = new Alg(vertices, adjacencyMatrix);
            int[,] incidenceMatrix = g.GetIncidenceMatrix();
            List<HashSet<int>> dnf = g.GetDNF(incidenceMatrix);
            g.WriteMissingVertices(ref dnf);
            dnf.Sort((s1, s2) => -1 * s1.Count.CompareTo(s2.Count));
            g.ColorGraph(dnf);

           
        }
    }
}

