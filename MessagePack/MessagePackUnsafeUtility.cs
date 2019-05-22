using System;
using System.Runtime.CompilerServices;

namespace MessagePack
{
    /// <summary>
    /// Provides unsafe utilities.
    /// </summary>
    public static class MessagePackUnsafeUtility
    {
        /// <summary>
        /// Reinterprets the given reference as a reference to a value of type <typeparamref name="TTo">TTo</typeparamref>.
        /// </summary>
        /// <param name="from">The reference to reinterpret.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ref TTo As<TTFrom, TTo>(ref TTFrom from)
        {
            // IL_0000: ldarg.0
            // IL_0001: ret

            throw new NotSupportedException("This method must be patched.");
        }
    }
}
