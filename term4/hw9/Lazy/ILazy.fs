namespace Lazy

module Interface =

    /// <summary>
    /// Interface for performing lazy evaluations
    /// </summary>
    type ILazy<'a> =

        /// <summary>
        /// Returns result of evaluation
        /// </summary>
        abstract member Get: unit -> 'a



