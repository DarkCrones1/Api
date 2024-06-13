using System.ComponentModel;

namespace Api.Common.Enumerations;

public enum LocalContainer
{
    [Description("Imagen de publicación")]
    Image_Post = 1,
    [Description("Imagenes de perfil")]
    Image_Profile = 2,
    [Description("Imagenes de comentario")]
    Image_Commentary = 3,
}