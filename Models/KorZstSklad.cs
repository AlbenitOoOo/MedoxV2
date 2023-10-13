using System;
using System.Collections.Generic;

namespace Medox.Models;

public partial class KorZstSklad
{
    /// <summary>
    /// Index jednoznacznie określający składnik w składzie zestawu.
    /// </summary>
    public int ZstSkladId { get; set; }

    /// <summary>
    /// Identyfikator modyfikacji składu zestawu
    /// </summary>
    public int ZstToZstSkladId { get; set; }

    /// <summary>
    /// Index składnika wchodzącego w skład zestawu.
    /// </summary>
    public int SkdId { get; set; }

    /// <summary>
    /// Ilość składników.
    /// </summary>
    public byte ZstSkladIloscSkd { get; set; }

    /// <summary>
    /// Uwagi do składnika wchodzącego w skład zestawu.
    /// </summary>
    public string? ZstSkladnikUwagi { get; set; }

    /// <summary>
    /// Określa czy dany składnik jest przypisany do zestawu (zarejestrowany). Domyślnie jest.
    /// </summary>
    public bool ZstSkladCzyAktywny { get; set; }

    public DateTime DbDataWpisu { get; set; }

    public short DbPerId { get; set; }

    public virtual KorSkd Skd { get; set; } = null!;

    public virtual KorZstToKorZstSklad ZstToZstSklad { get; set; } = null!;
}
