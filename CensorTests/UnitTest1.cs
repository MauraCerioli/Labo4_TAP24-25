using Censor;
using MessageInterface;
using Moq;

namespace CensorTests
{
    public class Tests {
        Mock<I>[] GenerateMocks(string[] source) {
            var result = new Mock<I>[source.Length];
            for (int i = 0; i < source.Length; i++) {
                result[i] = new Mock<I>();
                result[i].Setup(m => m.Message).Returns(source[i]);
            }
            return result;
        }

        /// <summary>
        /// Tests that when badWord is not used the result is the sequence itself
        /// <param name="source">sequence of strings that do not contain the badWord "cattivo</param>
        /// </summary>
        IEnumerable<Mock<I>> InfiniteSequenceOfMocks1(string[] source) {
            while (true) {
                foreach (var s in source) {
                    var x = new Mock<I>();
                    x.Setup(m => m.Message).Returns(s);
                    yield return x;
                }
            }
        }
        IEnumerable<Mock<I>> InfiniteSequenceOfMocks(string[] source) {
            var helper = GenerateMocks(source);
            while (true)
                foreach (var mock in helper)
                    yield return mock;
        }
        [TestCase(new string[]{"1","2","no","3"}, new string[]{"1", "2", "3"}, "no",2)]
        public void InfiniteOKWithFilter(string[] source, string[] filtered, string badWord, int checkedNumber) {
            var sequence = InfiniteSequenceOfMocks(source).Select(o=>o.Object);
            var expected = InfiniteSequenceOfMocks(filtered).Select(o => o.Object);
            var actual = sequence.Censor(badWord).Take(checkedNumber).ToArray();
            var exp = expected.Take(checkedNumber).ToArray();
            Assert.That(actual,Is.EqualTo(exp));
        }
        [TestCase()]
        [TestCase("puffo")]
        [TestCase("first","second")]
        [TestCase("1","2","3")]
        [TestCase("1", "2", "3","4","5","6","7")]
        public void TestOkCase(params string[] source) {
            var sequence = GenerateMocks(source).Select(o => o.Object);
            //var actual = CensorClass.Censor(sequence,"cattivo");
            var actual = sequence.Censor("cattivo");
            Assert.That(actual,Is.EqualTo(sequence));
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}