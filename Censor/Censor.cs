using MessageInterface;

namespace Censor {
    public static class CensorClass {
        public static IEnumerable<I> Censor(this IEnumerable<I>? sequence, string badWord) {
           /* if (null==localSequence)
                throw new ArgumentNullException(nameof(localSequence));*/
            ArgumentNullException.ThrowIfNull(sequence,nameof(sequence));
            return SafeCensor(sequence);

            IEnumerable<I> SafeCensor(IEnumerable<I> localSequence) {
                foreach (var m in localSequence) {
                    ArgumentNullException.ThrowIfNull(m);
                    /*if (m.Message.Contains(badWord)) continue;
                yield return m;*/
                    if (!m.Message.Contains(badWord))
                        yield return m;
                }
                /*
            using var en=localSequence.GetEnumerator();
            while (en.MoveNext()) {
                var m = en.Current;
                ArgumentNullException.ThrowIfNull(m);
                //if (m.Message.Contains(badWord)) continue;
                //yield return m;
                if (!m.Message.Contains(badWord))
                    yield return m;
            }*/
                //en.Dispose();
            }
        }
    }
}
