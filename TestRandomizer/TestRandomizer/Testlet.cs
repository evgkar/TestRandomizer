namespace TestRandomizer
{
    public class Testlet : ITestlet
    {
        private readonly IShuffler _shuffler;
        private readonly List<Item> _items;

        public string TestletId { get; }

        public Testlet(
            string testletId,
            List<Item> items,
            IShuffler shuffler)
        {
            TestletId = testletId;
            _items = items;
            _shuffler = shuffler;
        }

        public List<Item> Randomize()
        {
            //Items private collection has 6 Operational and 4 Pretest Items. Randomize the order of these items as per the requirement (with TDD)
            //The assignment will be reviewed on the basis of – Tests written first, Correct logic, Well structured & clean readable code.
            throw new NotImplementedException();
        }
    }
}
