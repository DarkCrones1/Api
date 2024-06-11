using System.ComponentModel;

namespace Api.Common.Enumerations;

public enum AzureContainer
{
    [Description("Identificaciones de cliente")]
    Customer_Identification = 1,
    [Description("Comprobantes de domicilio")]
    Customer_Proof_Address = 2,
    [Description("Imagenes de perfil")]
    Image_Profile = 3,
    [Description("Otros documentos de cliente")]
    Customer_Other_Documents = 4,
    [Description("Otros documentos de artesanos")]
    Craftman_Other_Documents = 5,
    [Description("Imagenes de Artesanias")]
    Image_Craft = 6
}
