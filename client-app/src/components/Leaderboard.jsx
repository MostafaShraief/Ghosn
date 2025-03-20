import React from "react";
import {
  List,
  ListItem,
  ListItemText,
  ListItemAvatar,
  Avatar,
  Typography,
  Box,
  Card,
  CardContent,
  Tooltip,
  useTheme,
  Divider, // Import Divider
  Badge, // Import Badge
} from "@mui/material";
import MilitaryTechIcon from "@mui/icons-material/MilitaryTech";
import EmojiEventsIcon from "@mui/icons-material/EmojiEvents";
import WorkspacePremiumIcon from "@mui/icons-material/WorkspacePremium";
import StarBorderIcon from "@mui/icons-material/StarBorder"; // Import StarBorderIcon
import TrendingUpIcon from "@mui/icons-material/TrendingUp"; // Import TrendingUp icon

const getRankingColor = (rank, theme) => {
  if (rank === 0) return theme.palette.gold.main;
  if (rank === 1) return theme.palette.silver.main;
  if (rank === 2) return theme.palette.bronze.main;
  return "transparent"; // No special background for other ranks
};

// More distinct icons
const getRankingIcon = (rank) => {
  if (rank === 0) return <EmojiEventsIcon />;
  if (rank === 1) return <WorkspacePremiumIcon />;
  if (rank === 2) return <MilitaryTechIcon />;
  return <StarBorderIcon />; // Default: star for all other ranks
};
const getRankingLabel = (rank) => {
  if (rank === 0) return "1st";
  if (rank === 1) return "2nd";
  if (rank === 2) return "3rd";
  return `${rank + 1}th`;
};

const Leaderboard = ({ clients }) => {
  const theme = useTheme();

  return (
    <List>
      {clients.map((client, index) => (
        <Card
          key={client.id}
          sx={{
            mb: 1, // Reduced margin for tighter spacing
            borderRadius: 2,
            boxShadow: index < 3 ? 3 : 1,
            border:
              index < 3 ? `2px solid ${getRankingColor(index, theme)}` : "none",
            transition:
              "transform 0.2s ease-in-out, box-shadow 0.2s ease-in-out",
            "&:hover": {
              transform: index < 3 ? "scale(1.02)" : "scale(1.01)",
              boxShadow: index < 3 ? 5 : 2,
            },
            backgroundColor:
              index < 3
                ? getRankingColor(index, theme) + "33"
                : theme.palette.background.paper, // Lighter background for top 3
          }}
        >
          <CardContent sx={{ p: 1, "&:last-child": { pb: 1 } }}>
            <ListItem disableGutters>
              <ListItemAvatar>
                <Badge
                  overlap="circular"
                  badgeContent={
                    <Tooltip title={getRankingLabel(index)}>
                      <Box sx={{ color: getRankingColor(index, theme) }}>
                        {getRankingIcon(index)}
                      </Box>
                    </Tooltip>
                  }
                >
                  <Avatar
                    sx={{
                      backgroundColor: theme.palette.grey[300], // Consistent background for avatar
                      color: theme.palette.getContrastText(
                        theme.palette.grey[300]
                      ), // Ensure text contrast
                      width: 40, // Slightly larger avatar
                      height: 40,
                    }}
                  >
                    {client.firstName.charAt(0)}
                    {client.lastName.charAt(0)}
                  </Avatar>
                </Badge>
              </ListItemAvatar>
              <ListItemText
                primary={`${client.name}`}
                secondary={
                  <Box display="flex" alignItems="center">
                    <TrendingUpIcon sx={{ mr: 0.5, color: "green" }} />{" "}
                    {/* Area icon */}
                    <Typography variant="body2" color="text.secondary">
                      {`المساحة: ${client.areaSize} متر مربع`}
                    </Typography>
                  </Box>
                }
                primaryTypographyProps={{
                  variant: "h6",
                  fontWeight: index < 3 ? 600 : 400, // Slightly less bold
                  noWrap: true, // Prevent name wrapping
                }}
                secondaryTypographyProps={{
                  variant: "body2",
                }}
              />
              <Box sx={{ display: "flex", alignItems: "center" }}>
                <Typography variant="h6" sx={{ ml: 1 }}>
                  {index + 1}
                </Typography>
              </Box>
            </ListItem>
          </CardContent>
          {index < clients.length - 1 && <Divider />}{" "}
          {/* Add divider between items */}
        </Card>
      ))}
    </List>
  );
};

export default Leaderboard;
