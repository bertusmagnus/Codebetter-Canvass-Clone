namespace CodeBetter.Canvas.Tests.Web.Binders.ModelBinderTests
{
    using System.Collections.Specialized;
    using Canvas.Web.Binders;
    using Rhino.Mocks;
    using Validation;
    using Xunit;

    public class BindModelTests : BaseFixture
    {
        [Fact]
        public void BindsNewEntityIfIdNotPresent()
        {
            Assert.Equal("new", new FakeBinder(null, null).BindModel(new NameValueCollection(), null));
        }
        [Fact]
        public void ReturnsNullIfEntityNotFound()
        {
            var repository = Dynamic<IRepository>();
            var parameters = new NameValueCollection { { "id", "5" } };
            ValidationError[] errors = null;

            repository.Expect(r => r.Find<string>(5)).Return(null);
            ReplayAll();

            Assert.Null(new FakeBinder(repository, null).BindModel(parameters, e => errors = e));
            repository.VerifyAllExpectations();
            Assert.Equal(1, errors.Length);
            Assert.Equal(null, errors[0].Field);
            Assert.Equal("Failed to bind", errors[0].Message);
        }
        [Fact]
        public void LoadsEntityFromId()
        {
            var repository = Dynamic<IRepository>();
            var parameters = new NameValueCollection { { "id", "4" } };

            repository.Expect(r => r.Find<string>(4)).Return("old");
            ReplayAll();

            Assert.Equal("The original string was: old", new FakeBinder(repository, null).BindModel(parameters, null));
            repository.VerifyAllExpectations();
        }
        [Fact]
        public void ValidatesEntity()
        {
            var validator = Dynamic<IValidator>();
            validator.Expect(v => v.Validate(Arg<string>.Is.Equal("new"), out Arg<ValidationError[]>.Out(new[] { new ValidationError("X", "Y") }).Dummy)).Return(false);
            ReplayAll();

            new FakeBinder(null, validator, true, false).BindModel(new NameValueCollection(), e =>
            {
                Assert.Equal("X", e[0].Field);
                Assert.Equal("Y", e[0].Message);
                Assert.Equal(1, e.Length);
            });
            validator.VerifyAllExpectations();
        }
        [Fact]
        public void AllowsCustomBinderToValidateEntity()
        {
            var validator = Dynamic<IValidator>();

            validator.Stub(v => v.Validate(Arg<string>.Is.Anything, out Arg<ValidationError[]>.Out(new[] { new ValidationError("X", "Y") }).Dummy)).Return(true);
            ReplayAll();

            var count = 0;
            new FakeBinder(null, validator, true, true).BindModel(new NameValueCollection(), e =>
            {
                if (count++ == 0)
                {
                    Assert.Equal("X", e[0].Field);
                    Assert.Equal("Y", e[0].Message);
                }
                else
                {
                    Assert.Equal("XX", e[1].Field);
                    Assert.Equal("YY", e[1].Message);
                }
            });
        }

        private class FakeBinder : ModelBinder<string>
        {
            private readonly bool _validate;
            private readonly bool _customValidator;
            public FakeBinder(IRepository repository, IValidator validator) : this(repository, validator, false, false) { }
            public FakeBinder(IRepository repository, IValidator validator, bool validate, bool customValidator) : base(repository, validator)
            {
                _validate = validate;
            }

            protected override bool ShouldValidate
            {
                get { return _validate; }
            }
            protected override ValidationError[] Validate(string entity)
            {
                return _customValidator ? new[] { new ValidationError("XX", "YY") } : null;
            }

            public override string BindNew()
            {
                return "new";
            }
            public override string BindExisting(string original)
            {
                return string.Concat("The original string was: ", original);
            }
        }
    }
}