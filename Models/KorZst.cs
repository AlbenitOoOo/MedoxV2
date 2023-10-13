using System;
using System.Collections.Generic;

namespace Medox.Models;

public partial class KorZst
{
    /// <summary>
    /// Identyfikator zestawu (Składniki też są traktowane jak zestawy)
    /// </summary>
    public int ZstId { get; set; }

    /// <summary>
    /// Identyfikator zestawu który był pierwszy raz tworzony. Jest powtarzany dla wszystkich modyfikacji - w ten sposób mamy historię modyfikacji zestawu.
    /// </summary>
    public int ZstMainId { get; set; }

    /// <summary>
    /// Identyfikator modyfikacji składu zestawu
    /// </summary>
    public int? ZstToZstSkladId { get; set; }

    public bool ZstCzyAktywny { get; set; }

    public DateTime DbDataWpisu { get; set; }

    public short DbPerId { get; set; }

    public virtual KorZstMain ZstMain { get; set; } = null!;

    public virtual KorZstToKorZstSklad? ZstToZstSklad { get; set; }
}
