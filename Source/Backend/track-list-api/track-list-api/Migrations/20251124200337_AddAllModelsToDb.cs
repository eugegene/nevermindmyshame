using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddAllModelsToDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Comment_ParentCommentId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Review_ReviewId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Users_UserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_CommentLike_Comment_CommentId",
                table: "CommentLike");

            migrationBuilder.DropForeignKey(
                name: "FK_Follow_Users_FollowerId",
                table: "Follow");

            migrationBuilder.DropForeignKey(
                name: "FK_Follow_Users_FollowingId",
                table: "Follow");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaTranslation_Media_MediaId",
                table: "MediaTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaTranslation_Users_ProcessedByUserId",
                table: "MediaTranslation");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlist_Users_OwnerId",
                table: "Playlist");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistAccess_Playlist_PlaylistId",
                table: "PlaylistAccess");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistItem_Media_MediaId",
                table: "PlaylistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistItem_Playlist_PlaylistId",
                table: "PlaylistItem");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Media_MediaId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_Review_Users_UserId",
                table: "Review");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewLike_Review_ReviewId",
                table: "ReviewLike");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewLike_Users_UserId",
                table: "ReviewLike");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackingStatus_Media_MediaId",
                table: "TrackingStatus");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackingStatus_Users_UserId",
                table: "TrackingStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackingStatus",
                table: "TrackingStatus");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewLike",
                table: "ReviewLike");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Review",
                table: "Review");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistItem",
                table: "PlaylistItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playlist",
                table: "Playlist");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaTranslation",
                table: "MediaTranslation");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follow",
                table: "Follow");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.RenameTable(
                name: "TrackingStatus",
                newName: "TrackingStatuses");

            migrationBuilder.RenameTable(
                name: "ReviewLike",
                newName: "ReviewLikes");

            migrationBuilder.RenameTable(
                name: "Review",
                newName: "Reviews");

            migrationBuilder.RenameTable(
                name: "PlaylistItem",
                newName: "PlaylistItems");

            migrationBuilder.RenameTable(
                name: "Playlist",
                newName: "Playlists");

            migrationBuilder.RenameTable(
                name: "MediaTranslation",
                newName: "MediaTranslations");

            migrationBuilder.RenameTable(
                name: "Follow",
                newName: "Follows");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameIndex(
                name: "IX_TrackingStatus_UserId",
                table: "TrackingStatuses",
                newName: "IX_TrackingStatuses_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TrackingStatus_MediaId",
                table: "TrackingStatuses",
                newName: "IX_TrackingStatuses_MediaId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewLike_UserId",
                table: "ReviewLikes",
                newName: "IX_ReviewLikes_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewLike_ReviewId",
                table: "ReviewLikes",
                newName: "IX_ReviewLikes_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_UserId",
                table: "Reviews",
                newName: "IX_Reviews_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Review_MediaId",
                table: "Reviews",
                newName: "IX_Reviews_MediaId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistItem_PlaylistId",
                table: "PlaylistItems",
                newName: "IX_PlaylistItems_PlaylistId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistItem_MediaId",
                table: "PlaylistItems",
                newName: "IX_PlaylistItems_MediaId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlist_OwnerId",
                table: "Playlists",
                newName: "IX_Playlists_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_MediaTranslation_ProcessedByUserId",
                table: "MediaTranslations",
                newName: "IX_MediaTranslations_ProcessedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MediaTranslation_MediaId",
                table: "MediaTranslations",
                newName: "IX_MediaTranslations_MediaId");

            migrationBuilder.RenameIndex(
                name: "IX_Follow_FollowingId",
                table: "Follows",
                newName: "IX_Follows_FollowingId");

            migrationBuilder.RenameIndex(
                name: "IX_Follow_FollowerId_FollowingId",
                table: "Follows",
                newName: "IX_Follows_FollowerId_FollowingId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ReviewId",
                table: "Comments",
                newName: "IX_Comments_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ParentCommentId",
                table: "Comments",
                newName: "IX_Comments_ParentCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackingStatuses",
                table: "TrackingStatuses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewLikes",
                table: "ReviewLikes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistItems",
                table: "PlaylistItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaTranslations",
                table: "MediaTranslations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follows",
                table: "Follows",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Reports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetId = table.Column<Guid>(type: "uuid", nullable: false),
                    TargetType = table.Column<int>(type: "integer", nullable: false),
                    Reason = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    ReporterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ProcessedByUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reports_Users_ProcessedByUserId",
                        column: x => x.ProcessedByUserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reports_Users_ReporterId",
                        column: x => x.ReporterId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFiles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserImages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    FileName = table.Column<string>(type: "text", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserImages_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ProcessedByUserId",
                table: "Reports",
                column: "ProcessedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Reports_ReporterId",
                table: "Reports",
                column: "ReporterId");

            migrationBuilder.CreateIndex(
                name: "IX_UserImages_UserId",
                table: "UserImages",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLike_Comments_CommentId",
                table: "CommentLike",
                column: "CommentId",
                principalTable: "Comments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments",
                column: "ParentCommentId",
                principalTable: "Comments",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Reviews_ReviewId",
                table: "Comments",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_Users_FollowerId",
                table: "Follows",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Follows_Users_FollowingId",
                table: "Follows",
                column: "FollowingId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaTranslations_Media_MediaId",
                table: "MediaTranslations",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaTranslations_Users_ProcessedByUserId",
                table: "MediaTranslations",
                column: "ProcessedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistAccess_Playlists_PlaylistId",
                table: "PlaylistAccess",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistItems_Media_MediaId",
                table: "PlaylistItems",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistItems_Playlists_PlaylistId",
                table: "PlaylistItems",
                column: "PlaylistId",
                principalTable: "Playlists",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlists_Users_OwnerId",
                table: "Playlists",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewLikes_Reviews_ReviewId",
                table: "ReviewLikes",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewLikes_Users_UserId",
                table: "ReviewLikes",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Media_MediaId",
                table: "Reviews",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingStatuses_Media_MediaId",
                table: "TrackingStatuses",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingStatuses_Users_UserId",
                table: "TrackingStatuses",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CommentLike_Comments_CommentId",
                table: "CommentLike");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Comments_ParentCommentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Reviews_ReviewId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_Users_FollowerId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_Follows_Users_FollowingId",
                table: "Follows");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaTranslations_Media_MediaId",
                table: "MediaTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_MediaTranslations_Users_ProcessedByUserId",
                table: "MediaTranslations");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistAccess_Playlists_PlaylistId",
                table: "PlaylistAccess");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistItems_Media_MediaId",
                table: "PlaylistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PlaylistItems_Playlists_PlaylistId",
                table: "PlaylistItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Playlists_Users_OwnerId",
                table: "Playlists");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewLikes_Reviews_ReviewId",
                table: "ReviewLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewLikes_Users_UserId",
                table: "ReviewLikes");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Media_MediaId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Users_UserId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackingStatuses_Media_MediaId",
                table: "TrackingStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_TrackingStatuses_Users_UserId",
                table: "TrackingStatuses");

            migrationBuilder.DropTable(
                name: "Reports");

            migrationBuilder.DropTable(
                name: "UserFiles");

            migrationBuilder.DropTable(
                name: "UserImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TrackingStatuses",
                table: "TrackingStatuses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Reviews",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewLikes",
                table: "ReviewLikes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Playlists",
                table: "Playlists");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PlaylistItems",
                table: "PlaylistItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MediaTranslations",
                table: "MediaTranslations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Follows",
                table: "Follows");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.RenameTable(
                name: "TrackingStatuses",
                newName: "TrackingStatus");

            migrationBuilder.RenameTable(
                name: "Reviews",
                newName: "Review");

            migrationBuilder.RenameTable(
                name: "ReviewLikes",
                newName: "ReviewLike");

            migrationBuilder.RenameTable(
                name: "Playlists",
                newName: "Playlist");

            migrationBuilder.RenameTable(
                name: "PlaylistItems",
                newName: "PlaylistItem");

            migrationBuilder.RenameTable(
                name: "MediaTranslations",
                newName: "MediaTranslation");

            migrationBuilder.RenameTable(
                name: "Follows",
                newName: "Follow");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameIndex(
                name: "IX_TrackingStatuses_UserId",
                table: "TrackingStatus",
                newName: "IX_TrackingStatus_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_TrackingStatuses_MediaId",
                table: "TrackingStatus",
                newName: "IX_TrackingStatus_MediaId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_UserId",
                table: "Review",
                newName: "IX_Review_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_MediaId",
                table: "Review",
                newName: "IX_Review_MediaId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewLikes_UserId",
                table: "ReviewLike",
                newName: "IX_ReviewLike_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewLikes_ReviewId",
                table: "ReviewLike",
                newName: "IX_ReviewLike_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Playlists_OwnerId",
                table: "Playlist",
                newName: "IX_Playlist_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistItems_PlaylistId",
                table: "PlaylistItem",
                newName: "IX_PlaylistItem_PlaylistId");

            migrationBuilder.RenameIndex(
                name: "IX_PlaylistItems_MediaId",
                table: "PlaylistItem",
                newName: "IX_PlaylistItem_MediaId");

            migrationBuilder.RenameIndex(
                name: "IX_MediaTranslations_ProcessedByUserId",
                table: "MediaTranslation",
                newName: "IX_MediaTranslation_ProcessedByUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MediaTranslations_MediaId",
                table: "MediaTranslation",
                newName: "IX_MediaTranslation_MediaId");

            migrationBuilder.RenameIndex(
                name: "IX_Follows_FollowingId",
                table: "Follow",
                newName: "IX_Follow_FollowingId");

            migrationBuilder.RenameIndex(
                name: "IX_Follows_FollowerId_FollowingId",
                table: "Follow",
                newName: "IX_Follow_FollowerId_FollowingId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comment",
                newName: "IX_Comment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ReviewId",
                table: "Comment",
                newName: "IX_Comment_ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ParentCommentId",
                table: "Comment",
                newName: "IX_Comment_ParentCommentId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TrackingStatus",
                table: "TrackingStatus",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Review",
                table: "Review",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewLike",
                table: "ReviewLike",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Playlist",
                table: "Playlist",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PlaylistItem",
                table: "PlaylistItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MediaTranslation",
                table: "MediaTranslation",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Follow",
                table: "Follow",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Comment_ParentCommentId",
                table: "Comment",
                column: "ParentCommentId",
                principalTable: "Comment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Review_ReviewId",
                table: "Comment",
                column: "ReviewId",
                principalTable: "Review",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Users_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CommentLike_Comment_CommentId",
                table: "CommentLike",
                column: "CommentId",
                principalTable: "Comment",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_Users_FollowerId",
                table: "Follow",
                column: "FollowerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Follow_Users_FollowingId",
                table: "Follow",
                column: "FollowingId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaTranslation_Media_MediaId",
                table: "MediaTranslation",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MediaTranslation_Users_ProcessedByUserId",
                table: "MediaTranslation",
                column: "ProcessedByUserId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Playlist_Users_OwnerId",
                table: "Playlist",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistAccess_Playlist_PlaylistId",
                table: "PlaylistAccess",
                column: "PlaylistId",
                principalTable: "Playlist",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistItem_Media_MediaId",
                table: "PlaylistItem",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PlaylistItem_Playlist_PlaylistId",
                table: "PlaylistItem",
                column: "PlaylistId",
                principalTable: "Playlist",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Media_MediaId",
                table: "Review",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Review_Users_UserId",
                table: "Review",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewLike_Review_ReviewId",
                table: "ReviewLike",
                column: "ReviewId",
                principalTable: "Review",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewLike_Users_UserId",
                table: "ReviewLike",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingStatus_Media_MediaId",
                table: "TrackingStatus",
                column: "MediaId",
                principalTable: "Media",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TrackingStatus_Users_UserId",
                table: "TrackingStatus",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
