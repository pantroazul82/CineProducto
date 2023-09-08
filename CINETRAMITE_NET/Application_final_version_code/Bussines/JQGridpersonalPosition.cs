using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CineProducto.Bussines
{
    public class JQGridpersonalPosition
    {
       #region Passive attributes.

    private int _pageCount;
    private int _currentPage;
    private int _recordCount;
    private List<JQGridItem> _items;

    #endregion

    #region Properties

    /// <summary>
    /// Cantidad de páginas del JQGrid.
    /// </summary>
    public int PageCount
    {
      get { return _pageCount; }
      set { _pageCount = value; }
    }
    /// <summary>
    /// Página actual del JQGrid.
    /// </summary>
    public int CurrentPage
    {
      get { return _currentPage; }
      set { _currentPage = value; }
    }
    /// <summary>
    /// Cantidad total de elementos de la lista.
    /// </summary>
    public int RecordCount
    {
      get { return _recordCount; }
      set { _recordCount = value; }
    }
    /// <summary>
    /// Lista de elementos del JQGrid.
    /// </summary>
    public List<JQGridItem> Items
    {
      get { return _items; }
      set { _items = value; }
    }

    #endregion

    #region Active attributes


    public JQGridpersonalPosition(int pPageCount, int pCurrentPage, int pRecordCount, List<position> positionOptions)
    {
        _pageCount = pPageCount;
        _currentPage = pCurrentPage;
        _recordCount = pRecordCount;
        _items = new List<JQGridItem>();
        foreach (position position in positionOptions)
            _items.Add(new JQGridItem(position.position_id, new List<string> { position.position_name, position.position_description,position.position_father_id.ToString() }));
    }
    #endregion  
    }
}