using System;
using System.Collections.Generic;

namespace Medox.Models;

public partial class KorZstMain
{
    /// <summary>
    /// Identyfikator zestawu który był pierwszy raz tworzony.
    /// </summary>
    public int ZstMainId { get; set; }

    /// <summary>
    /// Nazwa zestawu.
    /// </summary>
    public string ZstMainNazwa { get; set; } = null!;

    /// <summary>
    /// Opis zestawu.
    /// </summary>
    public string ZstMainOpis { get; set; } = null!;

    public bool ZstMainCzyAktywny { get; set; }

    public DateTime DbDataWpisu { get; set; }

    public short DbPerId { get; set; }

    public virtual ICollection<KorZst> KorZsts { get; set; } = new List<KorZst>();
}
