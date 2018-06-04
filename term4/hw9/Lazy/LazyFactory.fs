namespace Lazy

module Factory =
    open LazyImplementations
    open Interface
    /// <summary>
    /// Represents class used to create different types of classes performing lazy calculations
    /// </summary>
    type LazyFactory() =

        /// <summary>
        /// Creates class for single-threaded lazy evaluations
        /// </summary>
        static member CreateSingleThreadedLazy<'a> supplier =
            SingleThreadedLazy<'a> (supplier) :> ILazy<'a>

        /// <summary>
        /// Creates class for multi-threaded lazy evaluations
        /// </summary>
        static member CreateMultiThreadedLazy supplier =
            MultiThreadedLazy<'a> (supplier) :> ILazy<'a>

        /// <summary>
        /// Creates class for lock-free multi-threaded lazy evaluations
        /// </summary>
        static member CreateLockFreeLazy supplier =
            LockFreeLazy<'a> (supplier) :> ILazy<'a>
