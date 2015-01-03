using System;

namespace Pelorus.Core.Localization
{
    /// <summary>
    /// Method that transforms a DateTime value to a localized value.
    /// </summary>
    /// <param name="subject">DateTime value to transform.</param>
    /// <returns>Transformed DateTime value.</returns>
    public delegate DateTime LocalizerDelegate(DateTime subject);
}
