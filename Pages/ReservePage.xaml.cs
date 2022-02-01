﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UIKitTutorials.Entities;

namespace UIKitTutorials.Pages
{
    /// <summary>
    /// Логика взаимодействия для ReservePage.xaml
    /// </summary>
    public partial class ReservePage : Page
    {
        public ReservePage(Room room)
        {
            InitializeComponent();

            if (room is null)
            {
                return;
            }

            DataContext = room;
        }
    }
}
