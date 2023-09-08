using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

  /// <summary>
  /// Respuesta JSON para JQGrid.
  /// </summary>
  public class JQGridStaffOptionDetailJsonResponse
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


    public JQGridStaffOptionDetailJsonResponse(int pPageCount, int pCurrentPage, int pRecordCount, List<StaffOptionDetail> pChilds)
    {
        _pageCount = pPageCount;
        _currentPage = pCurrentPage;
        _recordCount = pRecordCount;
        _items = new List<JQGridItem>();
        foreach (StaffOptionDetail staff_option_detail in pChilds)
            _items.Add(new JQGridItem(staff_option_detail.id, new List<string> { staff_option_detail.position_name.ToString(), staff_option_detail.position_qty.ToString(), staff_option_detail.staff_option_id.ToString() }));
    }
    #endregion  
  }

