using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FluentAssertions;
using Xunit;
using static Niche.GherkinSyntax.GherkinFactory;

namespace Niche.GherkinSyntax.Tests
{
    [SuppressMessage(
        "Design",
        "RCS1090:Call 'ConfigureAwait(false)'.",
        Justification = "Not needed for tests.")]
    [SuppressMessage(
        "Reliability",
        "CA2007:Do not directly await a Task",
        Justification = "Not needed for tests.")]
    public class MetaTests
    {
        [Fact]
        public void SampleA()
            => Given(AnEmptyDocumentCache)
                .When(TheCacheGetsCleared)
                .Then(TheCacheShouldBeEmpty);

        [Fact]
        public void SampleB()
            => Given(AnEmptyDocumentCache)
                .And(ADocumentIsLoaded)
                .When(TheCacheGetsCleared)
                .Then(TheCacheShouldBeEmpty);

        private DocumentCache AnEmptyDocumentCache()
        {
            return new DocumentCache();
        }

        private DocumentCache ADocumentIsLoaded(DocumentCache cache)
        {
            cache.AddDocument("Sample");
            return cache;
        }

        private DocumentCache TheCacheGetsCleared(DocumentCache cache)
        {
            cache.Clear();
            return cache;
        }

        private void TheCacheShouldBeEmpty(DocumentCache cache)
        {
            cache.Documents.Should().BeEmpty();
        }

        private class DocumentCache
        {
            private readonly List<string> _documents = new List<string>();

            public IEnumerable<string> Documents => _documents;

            public void Clear() => _documents.Clear();

            public void AddDocument(string name) => _documents.Add(name);
        }
    }
}
