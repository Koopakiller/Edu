using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xml;

namespace StringAnalyzer.UiHelper
{
    /// <summary>
    /// Stellt Hilfsmethoden zum Sortieren der in einer <see cref="System.Windows.Controls.ListView"/> angezeigten Datensätze bereit.
    /// </summary>
    public class ListViewSort
    {
        #region DataTemplates

        /// <summary >Ruft das Template für einen GridViewColumnHeader mit einem hoch-pfeil ab.</summary>
        static readonly DataTemplate HeaderTemplateArrowUp = XamlReader.Load(XmlReader.Create(new StringReader(@"
<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">
    <DockPanel LastChildFill=""True"" Width=""{Binding ActualWidth, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type GridViewColumnHeader}}}"">
        <Path StrokeThickness=""1"" Fill=""Gray"" Data=""M 5,10 L 15,10 L 10,5 L 5,10"" DockPanel.Dock=""Right"" Width=""20"" HorizontalAlignment=""Right"" Margin=""5,0,5,0"" SnapsToDevicePixels=""True""/>
        <TextBlock Text=""{Binding }"" />
    </DockPanel>
</DataTemplate>"))) as DataTemplate;
        /// <summary >Ruft das Template für einen GridViewColumnHeader mit einem runter-pfeil ab.</summary>
        static readonly DataTemplate HeaderTemplateArrowDown = XamlReader.Load(XmlReader.Create(new StringReader(@"
<DataTemplate xmlns=""http://schemas.microsoft.com/winfx/2006/xaml/presentation"" xmlns:x=""http://schemas.microsoft.com/winfx/2006/xaml"">
    <DockPanel LastChildFill=""True"" Width=""{Binding ActualWidth, RelativeSource={RelativeSource FindAncestor, AncestorType={x:Type GridViewColumnHeader}}}"">
        <Path StrokeThickness=""1"" Fill=""Gray""  Data=""M 5,5 L 10,10 L 15,5 L 5,5"" DockPanel.Dock=""Right"" Width=""20"" HorizontalAlignment=""Right"" Margin=""5,0,5,0"" SnapsToDevicePixels=""True""/>
        <TextBlock Text=""{Binding }"" />
    </DockPanel>
</DataTemplate>"))) as DataTemplate;

        #endregion

        #region Get/Set for DPs

        /// <summary>
        /// Ruft den Wert der angefügten IsSortAtClickEnabled Abhängigkeitseigenschaft für eine bestimmte <see cref="System.Windows.Controls.GridViewColumn"/> ab.
        /// </summary>
        /// <param name="gvc">Die <see cref="System.Windows.Controls.GridViewColumn"/> deren IsSortAtClickEnabled Wert abgerufen werden soll.</param>
        public static bool GetIsSortAtClickEnabled(GridViewColumn gvc)
        {
            if (gvc == null) return false;
            return (bool)gvc.GetValue(IsSortAtClickEnabledProperty);
        }

        /// <summary>
        /// Setzt den Wert der angefügten IsSortAtClickEnabled Abhängigkeitseigenschaft für eine bestimmte <see cref="System.Windows.Controls.GridViewColumn"/>.
        /// </summary>
        /// <param name="gvc">Die <see cref="System.Windows.Controls.GridViewColumn"/> deren Wert gesetzt werdne soll.</param>
        /// <param name="value">Der zu setzende Wert. <c>true</c> wenn das Sortierren der Spalte erlaubt ist; andernfalls <c>false</c></param>
        public static void SetIsSortAtClickEnabled(GridViewColumn gvc, bool value)
        {
            gvc.SetValue(IsSortAtClickEnabledProperty, value);
        }

        /// <summary>
        /// Ruft den Wert der angefügten SortAtHeaderClick Abhängigkeitseigenschaft für eine bestimmte <see cref="System.Windows.Controls.ListView"/> ab.
        /// </summary>
        /// <param name="lv">Die <see cref="System.Windows.Controls.ListView"/> deren SortAtHeaderClick Wert abgerufen werden soll.</param>
        public static bool GetSortAtHeaderClick(ListView lv)
        {
            return (bool)lv.GetValue(SortAtHeaderClickProperty);
        }

        /// <summary>
        /// Setzt den Wert der angefügten SortAtHeaderClick Abhängigkeitseigenschaft für eine bestimmte <see cref="System.Windows.Controls.ListView"/>.
        /// </summary>
        /// <param name="lv">Die <see cref="System.Windows.Controls.ListView"/> deren Wert gesetzt werdne soll.</param>
        /// <param name="value">Der zu setzende Wert. <c>true</c> wenn das Sortierren des ListViews erlaubt ist; andernfalls <c>false</c></param>
        public static void SetSortAtHeaderClick(ListView lv, bool value)
        {
            lv.SetValue(SortAtHeaderClickProperty, value);
        }

        #endregion

        #region DependencyProperties

        /// <summary>
        /// Bezeichnet die angefügte IsSortAtClickEnabled Abhängigkeitseigenschaft.
        /// </summary>
        public static readonly DependencyProperty IsSortAtClickEnabledProperty = DependencyProperty.RegisterAttached("IsSortAtClickEnabled", typeof(bool), typeof(ListViewSort), new PropertyMetadata(true, OnIsSortAtClickEnabledChanged));
        /// <summary>
        /// Bezeichnet die angefügte SortAtHeaderClick Abhängigkeitseigenschaft.
        /// </summary>
        public static readonly DependencyProperty SortAtHeaderClickProperty = DependencyProperty.RegisterAttached("SortAtHeaderClick", typeof(bool), typeof(ListViewSort), new PropertyMetadata(false, OnSortAtHeaderClickChanged));

        #endregion

        #region DP Changed handler

        /// <summary>Wird aufgerufen, wenn sich die IsSortAtClickEnabled-Eigenshcat ändert.</summary>
        private static void OnIsSortAtClickEnabledChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((bool)e.NewValue != false) return;
            var col = d as GridViewColumn;

            foreach (var item in _lastClickedHeaders.Where(item => item.Value != null && item.Value.Column == col))
            {
                col.HeaderTemplate = null;
                ClearSortRules(item.Key.Target as ListView);
            }
        }

        /// <summary>Wird aufgerufen, wenn sich die SortAtHeaderClick Eigenschaft ändert</summary>
        private static void OnSortAtHeaderClickChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var lv = d as ListView;
            if ((bool)e.NewValue == true)
            {
                _lastClickedHeaders.Add(new WeakReference(lv), null);
                _lastDirections.Add(new WeakReference(lv), ListSortDirectionEx.Ascending);
                lv.AddHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnClicked));
            }
            else
            {
                lv.RemoveHandler(ButtonBase.ClickEvent, new RoutedEventHandler(ColumnClicked));
                var gv = lv.View as GridView;
                if (gv != null)
                    foreach (var col in gv.Columns)
                        col.HeaderTemplate = null;
                ClearSortRules(lv);
                _lastClickedHeaders.Remove(_lastClickedHeaders.FirstOrDefault(x => x.Key.Target == lv).Key);
                _lastDirections.Remove(_lastDirections.FirstOrDefault(x => x.Key.Target == lv).Key);
            }
        }

        #endregion

        /// <summary>Enthält eine Liste der zuletzt angeklickten Spaltenüberschriften.</summary>
        static readonly Dictionary<WeakReference, GridViewColumnHeader> _lastClickedHeaders = new Dictionary<WeakReference, GridViewColumnHeader>();
        /// <summary>Enthält eine Liste der gesetzten Ordnungsreihenfolgen.</summary>
        static readonly Dictionary<WeakReference, ListSortDirectionEx> _lastDirections = new Dictionary<WeakReference, ListSortDirectionEx>();

        /// <summary>Wird aufgerufen, wenn der Benutzer auf eine Spaltenüberschrift in einem ListView klickt, für das Sortierung möglich ist.</summary>
        private static void ColumnClicked(object sender, RoutedEventArgs e)
        {
            var headerClicked = e.OriginalSource as GridViewColumnHeader;
            var direction = ListSortDirectionEx.None;
            var lv = sender as ListView;

            var lastClickedHeader = _lastClickedHeaders.FirstOrDefault(x => x.Key.Target == sender).Value;
            var lastDirection = _lastDirections.FirstOrDefault(x => x.Key.Target == sender).Value;

            if (headerClicked == null || !GetIsSortAtClickEnabled(headerClicked.Column)) return;
            {
                if (headerClicked.Role == GridViewColumnHeaderRole.Padding) return;
                if (headerClicked != lastClickedHeader)
                {
                    direction = ListSortDirectionEx.Ascending;
                }
                else
                {
                    switch (lastDirection)
                    {
                        case ListSortDirectionEx.Ascending:
                            direction = ListSortDirectionEx.Descending;
                            break;
                        case ListSortDirectionEx.Descending:
                            direction = ListSortDirectionEx.None;
                            break;
                        case ListSortDirectionEx.None:
                            direction = ListSortDirectionEx.Ascending;
                            break;
                    }
                }

                if (direction == ListSortDirectionEx.None)
                {
                    ClearSortRules(lv);
                }
                else
                {
                    Sort((headerClicked.Column.DisplayMemberBinding as Binding).Path.Path, (ListSortDirection)direction, lv);
                }

                switch (direction)
                {
                    case ListSortDirectionEx.Ascending:
                        headerClicked.Column.HeaderTemplate = HeaderTemplateArrowUp;
                        break;
                    case ListSortDirectionEx.Descending:
                        headerClicked.Column.HeaderTemplate = HeaderTemplateArrowDown;
                        break;
                    case ListSortDirectionEx.None:
                        headerClicked.Column.HeaderTemplate = null;
                        break;
                }

                // Remove arrow from previously sorted header
                if (lastClickedHeader != null && lastClickedHeader != headerClicked)
                {
                    lastClickedHeader.Column.HeaderTemplate = null;
                }
                _lastClickedHeaders[_lastClickedHeaders.FirstOrDefault(x => x.Key.Target == sender).Key] = headerClicked;
                _lastDirections[_lastDirections.FirstOrDefault(x => x.Key.Target == sender).Key] = direction;
            }
        }

        /// <summary>Sortiert das angegebene ListView nach der angegebenen Eigenschaft und in der angegebenen Reihenfolge.</summary>
        private static void Sort(string sortBy, ListSortDirection direction, ItemsControl lv)
        {
            var dataView = CollectionViewSource.GetDefaultView(lv.ItemsSource);
            if (dataView == null) return;

            dataView.SortDescriptions.Clear();
            var sd = new SortDescription(sortBy, direction);
            dataView.SortDescriptions.Add(sd);
            dataView.Refresh();
        }
        /// <summary>Setzt die Sortierung für das angegebene ListView zurück.</summary>
        private static void ClearSortRules(ListView lv)
        {
            var dataView = CollectionViewSource.GetDefaultView(lv.ItemsSource);
            if (dataView == null) return;
            dataView.SortDescriptions.Clear();
            dataView.Refresh();
        }

        #region Types

        /// <summary>Die Möglichen Sortierreihenfolgen.</summary>
        private enum ListSortDirectionEx
        {
            Ascending = ListSortDirection.Ascending,
            Descending = ListSortDirection.Descending,
            None,
        }

        #endregion
    }

}
