// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using de_training;

#nullable disable

namespace de_training.Migrations
{
    [DbContext(typeof(libraryContext))]
    partial class libraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.9");

            modelBuilder.Entity("de_training.Book", b =>
                {
                    b.Property<long>("BookId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<long>("ReaderId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("BookId");

                    b.HasIndex(new[] { "ReaderId" }, "IX_Books_ReaderId");

                    b.ToTable("Books");
                });

            modelBuilder.Entity("de_training.Reader", b =>
                {
                    b.Property<long>("ReaderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Pib")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasColumnName("PIB");

                    b.HasKey("ReaderId");

                    b.ToTable("Readers");
                });

            modelBuilder.Entity("de_training.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("de_training.Book", b =>
                {
                    b.HasOne("de_training.Reader", "Reader")
                        .WithMany("Books")
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Reader");
                });

            modelBuilder.Entity("de_training.Reader", b =>
                {
                    b.Navigation("Books");
                });
#pragma warning restore 612, 618
        }
    }
}
