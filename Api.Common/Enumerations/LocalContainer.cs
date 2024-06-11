using System.ComponentModel;

namespace Api.Common.Enumerations;

public enum LocalContainer
{
    [Description("Imagenes de artesanias")]
    Image_Craft = 1,
    [Description("Imagenes de perfil")]
    Image_Profile = 2,
    [Description("Imagen de categorias")]
    Image_Category = 3,
    [Description("Otros documentos de cliente")]
    Customer_Other_Documents = 4,
    [Description("Otros documentos de artesanos")]
    Craftman_Other_Documents = 5,
}