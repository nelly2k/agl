using Xunit;
using agl.app;
using FizzWare.NBuilder;
using System.Linq;

namespace agl.test
{
    public class CatGenderBuilderUnitTests
    {
        private const string CAT = "Cat";
        private const string OTHER_PET = "Other Pet";

        private const string GENDER1 = "Gender 1";
        private const string GENDER2 = "Gender 2";

        private const string NAME1 = "Daenerys";
        private const string NAME2 = "Snow";

        private readonly CatGenderBuilder _catGenderBuilder;

        public CatGenderBuilderUnitTests()
        {
            _catGenderBuilder = new CatGenderBuilder();
        }

        [Fact]
        public void OneGender_OneCat_SingleGender()
        {
            var humans = Builder<Person>.CreateListOfSize(1)
                .All()
                .With(x => x.Gender = GENDER1)
                .With(x => x.Pets = Builder<Pet>.CreateListOfSize(1)
                    .All().With(c => c.Type = CAT).Build())
                .Build();

            var result = _catGenderBuilder.Execute(humans);

            Assert.Single(result[GENDER1]);
        }

        [Fact]
        public void OneGender_OneCat_SinglePet()
        {
            var humans = Builder<Person>.CreateListOfSize(1)
                .All()
                .With(x => x.Gender = GENDER1)
                .With(x => x.Pets = Builder<Pet>.CreateListOfSize(1)
                    .All().With(c => c.Type = CAT).Build())
                .Build();

            var result = _catGenderBuilder.Execute(humans);

            Assert.Single(result);
        }

        [Fact]
        public void OneGender_TwoPetsOneCat_SinglePet()
        {
            var humans = Builder<Person>.CreateListOfSize(1)
                .All()
                .With(x => x.Gender = GENDER1)
                .With(x => x.Pets = Builder<Pet>.CreateListOfSize(2)
                    .TheFirst(1).With(c => c.Type = CAT)
                    .TheNext(1).With(c => c.Type = OTHER_PET).Build())
                .Build();

            var result = _catGenderBuilder.Execute(humans);

            Assert.Single(result[GENDER1]);
        }

        [Fact]
        public void TwoGenders_OneCat_Each_TwoGendersAddedWithOnePet()
        {
            var humans = Builder<Person>.CreateListOfSize(2)
                .All().With(x => x.Pets = Builder<Pet>.CreateListOfSize(1)
                        .All().With(c => c.Type = CAT)
                        .Build())
                .TheFirst(1).With(x => x.Gender = GENDER1)
                .TheNext(1).With(x => x.Gender = GENDER2)
                .Build();

            var result = _catGenderBuilder.Execute(humans);

            Assert.Single(result[GENDER1]);
            Assert.Single(result[GENDER2]);
        }

        [Fact]
        public void OneGender_PetsAreNull_NoGenderAdded()
        {
            var humans = Builder<Person>.CreateListOfSize(1)
                .All()
                    .With(x => x.Gender = GENDER1)
                    .With(x => x.Pets = null)
                .Build();

            var result = _catGenderBuilder.Execute(humans);

            Assert.False(result.ContainsKey(GENDER1));
        }

        [Fact]
        public void OneGender_AllPetsArentCat_NoGenderAdded()
        {
            var humans = Builder<Person>.CreateListOfSize(1)
                .All()
                    .With(x => x.Gender = GENDER1)
                    .With(x => x.Pets = x.Pets = Builder<Pet>.CreateListOfSize(1)
                    .All().With(c => c.Type = GENDER2).Build())
                .Build();

            var result = _catGenderBuilder.Execute(humans);

            Assert.False(result.ContainsKey(GENDER1));
        }

        [Fact]
        public void OneGender_OneCat_NameMapped()
        {
            var humans = Builder<Person>.CreateListOfSize(1)
                .All()
                .With(x => x.Gender = GENDER1)
                .With(x => x.Pets = Builder<Pet>.CreateListOfSize(1)
                    .All()
                        .With(c => c.Type = CAT)
                        .With(c => c.Name = NAME1)
                        .Build())
                .Build();

            var result = _catGenderBuilder.Execute(humans);

            Assert.Equal(NAME1, result[GENDER1].First());
        }

        [Fact]
        public void OneGender_TwoCats_NameOrdered()
        {
            var humans = Builder<Person>.CreateListOfSize(1)
                .All()
                .With(x => x.Gender = GENDER1)
                .With(x => x.Pets = Builder<Pet>.CreateListOfSize(2)
                    .All().With(c => c.Type = CAT)
                    .TheFirst(1).With(c => c.Name = NAME2)
                    .TheNext(1).With(c => c.Name = NAME1)
                        .Build())
                .Build();

            var result = _catGenderBuilder.Execute(humans);

            Assert.Equal(NAME1, result[GENDER1].First());
        }

        [Fact]
        public void TwoGenders_OneNameEach_NameOrdered()
        {
            var humans = Builder<Person>.CreateListOfSize(2)
                .All().With(x => x.Gender = GENDER1)
                .TheFirst(1).With(x => x.Pets = Builder<Pet>.CreateListOfSize(1)
                    .All().With(p => p.Type = CAT)
                    .With(p => p.Name = NAME2).Build())
                .TheNext(1).With(x => x.Pets = Builder<Pet>.CreateListOfSize(1)
                    .All().With(p => p.Type = CAT)
                    .With(p => p.Name = NAME1).Build())
                .Build();

            var result = _catGenderBuilder.Execute(humans);

            Assert.Equal(NAME1, result[GENDER1].First());
        }
    }
}
