﻿using GetRoadRunner.Models;
using GetRoadRunner.Models.Graph;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GetRoadRunner.Controllers
{
    class Utils
    {
        public void MostrarTabuleiro(Vertice[,] matrizDeVertice, DataGridView dataGridView)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();

            int numLinhas = matrizDeVertice.GetLength(0);
            int numColunas = matrizDeVertice.GetLength(1);

            for (int coluna = 0; coluna < numColunas; coluna++)
            {
                dataGridView.Columns.Add($"col{coluna}", (coluna  + 1).ToString());
                dataGridView.Columns[coluna].Width = 30;
                //dataGridView1.Columns[coluna].Visible = false;
            }

            dataGridView.Rows.Add(numLinhas - 1);

            int count = 0;
            for (int row = 0; row < numLinhas; row++)
            {
                var arrItens = new int[numColunas];
                for (int c = 0; c < numColunas; c++)
                {
                    dataGridView.Rows[row].Cells[c].Value = matrizDeVertice[count, c].Visualizacao;
                    dataGridView.Rows[row].Cells[c].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;

                    switch (matrizDeVertice[count, c].Nome)
                    {
                        case Pecas.Asphalt:
                            dataGridView.Rows[row].Cells[c].Style.BackColor = Color.DarkGray;
                            break;

                        case Pecas.Grass:
                            dataGridView.Rows[row].Cells[c].Style.BackColor = Color.Green;
                            break;

                        case Pecas.Obstacle:
                            dataGridView.Rows[row].Cells[c].Style.BackColor = Color.Black;
                            break;

                        case Pecas.Sand:
                            dataGridView.Rows[row].Cells[c].Style.BackColor = Color.DarkKhaki;
                            break;

                        case Pecas.Water:
                            dataGridView.Rows[row].Cells[c].Style.BackColor = Color.RoyalBlue;
                            break;

                        case Pecas.Papaleguas:
                            //((DataGridViewImageCell)dataGridView1.Rows[row].Cells[c]).Value = Resources.Papaleguas as Bitmap;
                            dataGridView.Rows[row].Cells[c].Value = "P";
                            dataGridView.Rows[row].Cells[c].Style.BackColor = Color.DarkRed;
                            break;
                        case Pecas.Coyote:
                            //((DataGridViewImageCell)dataGridView1.Rows[row].Cells[c]).Value = Resources.Coiote as Bitmap;
                            dataGridView.Rows[row].Cells[c].Value = "C";
                            dataGridView.Rows[row].Cells[c].Style.BackColor = Color.DarkSeaGreen;
                            break;
                    }

                }
                count++;
            }
        }

    }
}