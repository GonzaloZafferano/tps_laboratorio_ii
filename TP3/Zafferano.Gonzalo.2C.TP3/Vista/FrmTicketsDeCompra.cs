﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Entidades;

namespace Vista
{
    public partial class FrmTicketsDeCompra : Form
    {
        private Administrador administrador;
        private Dictionary<string, string> tickets;
        public FrmTicketsDeCompra(Administrador administrador, Dictionary<string, string> tickets)
        {
            InitializeComponent();
            this.administrador = administrador;
            this.tickets = tickets;
        }

        private void FrmTicketsDeCompra_Load(object sender, EventArgs e)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"Menu Tickets. {this.administrador.Puesto}: ");
            sb.Append($"{this.administrador.NombreUsuario} ");
            sb.Append($"({this.administrador.NombreCompleto})");

            this.Text = sb.ToString();

            this.CargarComboBox();
        }

        /// <summary>
        /// Carga el comboBox con los nombres de los archivos de tickets.
        /// </summary>
        private void CargarComboBox()
        {
            foreach(string key in this.tickets.Keys)
            {
                this.cmbBoxTickets.Items.Add(key);
            }
        }

        private void cmbBoxTickets_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cmbBoxTickets.SelectedItem != null)
            {
                foreach(KeyValuePair<string, string> ticket in this.tickets)
                {
                    if(ticket.Key == cmbBoxTickets.Text)
                    {
                        this.rTxtTicket.Text = ticket.Value;
                        break;
                    }
                }
            }
        }

        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
