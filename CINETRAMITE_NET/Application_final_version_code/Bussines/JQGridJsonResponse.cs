using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CineProducto.Bussines;

  /// <summary>
  /// Respuesta JSON para JQGrid.
  /// </summary>
  public class JQGridJsonResponse
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

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
    public JQGridJsonResponse(int pPageCount, int pCurrentPage, int pRecordCount, List<StaffOption> pStaffOptions)
    {
        _pageCount = pPageCount;
        _currentPage = pCurrentPage;
        _recordCount = pRecordCount;
        _items = new List<JQGridItem>();
        foreach (StaffOption staff_option in pStaffOptions)
            _items.Add(new JQGridItem(staff_option.id, new List<string> { staff_option.production_type_name, staff_option.project_type_name, staff_option.project_genre_name, staff_option.has_domestic_director_description, staff_option.description, staff_option.staff_option_personal_option.ToString(), staff_option.staff_option_percentage_init.ToString(),staff_option.staff_option_percentage_end.ToString() }));
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
    public JQGridJsonResponse(int pPageCount, int pCurrentPage, int pRecordCount, List<Format> formatOptions)
    {
        _pageCount = pPageCount;
        _currentPage = pCurrentPage;
        _recordCount = pRecordCount;
        _items = new List<JQGridItem>();
        foreach (Format format in formatOptions)
            _items.Add(new JQGridItem(format.format_id, new List<string> {format.format_type_name.ToString(), format.format_name }));
    }
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
    public JQGridJsonResponse(int pPageCount, int pCurrentPage, int pRecordCount, List<position> positionOptions)
    {
        _pageCount = pPageCount;
        _currentPage = pCurrentPage;
        _recordCount = pRecordCount;
        _items = new List<JQGridItem>();
        foreach (position position in positionOptions)
           _items.Add(new JQGridItem(position.position_id, new List<string> {position.position_name, position.position_description }));
    }
    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
    public JQGridJsonResponse(int pPageCount, int pCurrentPage, int pRecordCount, List<Attachment> pAttachmentOptions)
    {
        _pageCount = pPageCount;
        _currentPage = pCurrentPage;
        _recordCount = pRecordCount;
        _items = new List<JQGridItem>();
        foreach (Attachment attachment_option in pAttachmentOptions)
            _items.Add(new JQGridItem(attachment_option.attachment_id, new List<string> { attachment_option.attachment_name, attachment_option.attachment_description, attachment_option.attachment_section.ToString(), attachment_option.attachment_format, attachment_option.attachment_order.ToString(),attachment_option.attachment_to_foreing_producer }));
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
    public JQGridJsonResponse(int pPageCount, int pCurrentPage, int pRecordCount, List<Role> pRoleOptions)
    {
        _pageCount = pPageCount;
        _currentPage = pCurrentPage;
        _recordCount = pRecordCount;
        _items = new List<JQGridItem>();
        foreach (Role option in pRoleOptions)
            _items.Add(new JQGridItem(option.role_id, new List<string> { option.role_name.ToString(), option.role_id.ToString() }));
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
    public JQGridJsonResponse(int pPageCount, int pCurrentPage, int pRecordCount, List<User> pAssignedUsers)
    {
        _pageCount = pPageCount;
        _currentPage = pCurrentPage;
        _recordCount = pRecordCount;
        _items = new List<JQGridItem>();
        foreach (User option in pAssignedUsers)
            _items.Add(new JQGridItem(option.user_id, new List<string> { option.user_id.ToString(), option.username.ToString() }));
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
    public JQGridJsonResponse(int pPageCount, int pCurrentPage, int pRecordCount, List<Permission> pPermission)
    {
        _pageCount = pPageCount;
        _currentPage = pCurrentPage;
        _recordCount = pRecordCount;
        _items = new List<JQGridItem>();
        foreach (Permission option in pPermission)
            _items.Add(new JQGridItem(option.permission_id, new List<string> { option.permission_name.ToString(), option.permission_description.ToString() }));
    }

    /// <summary>
    /// Constructor.
    /// </summary>
    /// <param name="pItems">Lista de elementos a mostrar en el JQGrid</param>
    public JQGridJsonResponse(int pPageCount, int pCurrentPage, int pRecordCount, List<ValidationAttachment> pValidationAttachment)
    {
        _pageCount = pPageCount;
        _currentPage = pCurrentPage;
        _recordCount = pRecordCount;
        _items = new List<JQGridItem>();
        foreach (ValidationAttachment option in pValidationAttachment) 
        {
            string active = (option.active == 1)?"Si":"No";
            string variable = "";
            string validation_type = "";

            switch(option.validation_variable)
            {
                case "producer_type":
                    variable = "Tipo de productor";
                    break;
                case "total_cost":
                    variable = "Costo total";
                    break;
                case "project_genre":
                    variable = "Tipo de proyecto";
                    break;
                case "domestic_producers_qty":
                    variable = "Cantidad de productores nacionales";
                    break;
                case "type_company":
                    variable = "Tipo de empresa";
                    break;
                case "project_type":
                    variable = "Duración";
                    break;
                case "production_type":
                    variable = "Tipo de Producción";
                    break;
                default:
                    break;
            }
            switch (option.validation_type)
            {
                case "required":
                    validation_type = "Requerido";
                    break;
                case "optional":
                    validation_type = "Opcional";
                    break;
                case "excluded":
                    validation_type = "Excluido";
                    break;
                default:
                    break;
            }
            _items.Add(new JQGridItem(option.validation_id, new List<string> { option.attachment_name.ToString(),
                                                                               variable,
                                                                               validation_type,
                                                                               option.validation_value.ToString(),
                                                                               option.validation_operator.ToString(),
                                                                               active }));
        }
    }
    #endregion  
  }