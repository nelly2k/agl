using System;
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
        private const string MALE = "Male";
        private const string OTHER_GENDER = "Other Gender";

        private const string NAME1 = "Daenerys";
        private const string NAME2 = "Snow";
        CatGenderBuilder catGenderBuilder;
        public CatGenderBuilderUnitTests()
        {
            catGenderBuilder = new CatGenderBuilder();
        }

        [Fact]
        public void OneGender_OneCat()
        {
            var humans = Builder<Human>.CreateListOfSize(1)
                .All()
                .With(x => x.Gender = MALE)
                .With(x => x.Pets = Builder<Pet>.CreateListOfSize(1)
                    .All().With(c => c.Type = CAT).Build())
                .Build();

            var result = catGenderBuilder.Execute(humans);

            Assert.Equal(result[MALE].Count(), 1);
        }

        [Fact]
        public void OneGender_TwoPetsOneCat_OneGenderWithPetAdded()
        {
            var humans = Builder<Human>.CreateListOfSize(1)
                .All()
                .With(x => x.Gender = MALE)
                .With(x => x.Pets = Builder<Pet>.CreateListOfSize(2)
                    .TheFirst(1).With(c => c.Type = CAT)
                    .TheNext(1).With(c => c.Type = OTHER_PET).Build())
                .Build();

            var result = catGenderBuilder.Execute(humans);

            Assert.Equal(result[MALE].Count(), 1);
        }

        [Fact]
        public void TwoGenders_OneCat_Each_TwoGendersAddedWithOnePet()
        {
            var humans = Builder<Human>.CreateListOfSize(2)
                .All().With(x => x.Pets = Builder<Pet>.CreateListOfSize(1)
                        .All().With(c => c.Type = CAT)
                        .Build())
                .TheFirst(1).With(x => x.Gender = MALE)
                .TheNext(1).With(x => x.Gender = OTHER_GENDER)
                .Build();

            var result = catGenderBuilder.Execute(humans);

            Assert.Equal(result[MALE].Count(), 1);
            Assert.Equal(result[OTHER_GENDER].Count(), 1);
        }

        [Fact]
        public void OneGender_PetsAreNull_NoGenderAdded()
        {
            var humans = Builder<Human>.CreateListOfSize(1)
                .All()
                    .With(x => x.Gender = MALE)
                    .With(x => x.Pets = null)
                .Build();

            var result = catGenderBuilder.Execute(humans);

            Assert.False(result.ContainsKey(MALE));
        }

        [Fact]
        public void OneGender_AllPetsArerentCat_NotGenderAdded()
        {
            var humans = Builder<Human>.CreateListOfSize(1)
                .All()
                    .With(x => x.Gender = MALE)
                    .With(x => x.Pets = x.Pets = Builder<Pet>.CreateListOfSize(1)
                    .All().With(c => c.Type = OTHER_GENDER).Build())
                .Build();

            var result = catGenderBuilder.Execute(humans);

            Assert.False(result.ContainsKey(MALE));
        }

        [Fact]
        public void OneGender_OneCat_NameMapped()
        { 
            var humans = Builder<Human>.CreateListOfSize(1)
                .All()
                .With(x => x.Gender = MALE)
                .With(x => x.Pets = Builder<Pet>.CreateListOfSize(1)
                    .All()
                        .With(c => c.Type = CAT)
                        .With(c => c.Name = NAME1)
                        .Build())
                .Build();

            var result = catGenderBuilder.Execute(humans);

            Assert.Equal(result[MALE].First(), NAME1);
        }

        [Fact]
        public void OneGender_TwoCats_NameOrdered()
        { 
            var humans = Builder<Human>.CreateListOfSize(1)
                .All()
                .With(x => x.Gender = MALE)
                .With(x => x.Pets = Builder<Pet>.CreateListOfSize(2)
                    .All().With(c => c.Type = CAT)
                    .TheFirst(1).With(c => c.Name = NAME2)
                    .TheNext(1).With(c => c.Name = NAME1)
                        .Build())
                .Build();

            var result = catGenderBuilder.Execute(humans);

            Assert.Equal(result[MALE].First(), NAME1);
        }

         [Fact]
        public void TwoGenders_OneNameEach_NameOrdered()
        { 
            var humans = Builder<Human>.CreateListOfSize(2)
                .All().With(x => x.Gender = MALE)
                .TheFirst(1).With(x=>x.Pets = Builder<Pet>.CreateListOfSize(1)
                    .All().With(p=>p.Type = CAT)
                    .With(p=>p.Name = NAME2).Build())
                .TheNext(1).With(x=>x.Pets = Builder<Pet>.CreateListOfSize(1)
                    .All().With(p=>p.Type = CAT)
                    .With(p=>p.Name = NAME1).Build())
                .Build();

            var result = catGenderBuilder.Execute(humans);

            Assert.Equal(result[MALE].First(), NAME1);
        }
    }
}
