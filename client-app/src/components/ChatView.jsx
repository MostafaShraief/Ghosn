import React, { useState, useRef, useEffect } from "react";
import Box from "@mui/material/Box";
import TextField from "@mui/material/TextField";
import SendIcon from "@mui/icons-material/Send";
import Typography from "@mui/material/Typography";
import Avatar from "@mui/material/Avatar";
import Paper from "@mui/material/Paper";
import { deepOrange, lightBlue } from "@mui/material/colors";
import { IconButton } from "@mui/material";

function ChatView() {
  const [messages, setMessages] = useState([
    {
      text: "This AI chatbot has been developed to optimize communication and simplify work processes, ultimately leading to smoother operations.",
      sender: "ai",
    },
    { text: "Thank You", sender: "user" },
  ]);
  const [newMessage, setNewMessage] = useState("");
  const messagesEndRef = useRef(null);

  const handleSendMessage = () => {
    if (newMessage.trim() !== "") {
      setMessages([...messages, { text: newMessage, sender: "user" }]);
      setNewMessage("");
      setTimeout(() => {
        setMessages((prev) => [
          ...prev,
          { text: "I'm a simulated AI response!", sender: "ai" },
        ]);
      }, 500);
    }
  };

  const handleInputChange = (event) => setNewMessage(event.target.value);
  const handleKeyPress = (event) => {
    if (event.key === "Enter" && !event.shiftKey) {
      event.preventDefault();
      handleSendMessage();
    }
  };

  useEffect(() => {
    scrollToBottom();
  }, [messages]);

  const scrollToBottom = () => {
    messagesEndRef.current?.scrollIntoView({ behavior: "smooth" });
  };

  return (
    <Box sx={{ height: "100%", display: "flex", flexDirection: "column" }}>
      <Typography
        variant="h5"
        sx={{
          mb: 3,
          fontWeight: 600,
          color: "primary.main",
        }}
      >
        Chat
      </Typography>

      <Paper
        elevation={1}
        sx={{
          flex: 1,
          overflowY: "auto",
          p: 3,
          mb: 2,
          bgcolor: "white",
          borderRadius: 2,
        }}
      >
        {messages.map((message, index) => (
          <Box
            key={index}
            sx={{
              display: "flex",
              alignItems: "flex-end",
              justifyContent:
                message.sender === "user" ? "flex-end" : "flex-start",
              mb: 2,
            }}
          >
            {message.sender === "ai" && (
              <Avatar
                sx={{ bgcolor: lightBlue[500], mr: 1, width: 32, height: 32 }}
              >
                AI
              </Avatar>
            )}
            <Paper
              elevation={0}
              sx={{
                p: 2,
                maxWidth: "70%",
                borderRadius: 3,
                color: message.sender === "user" ? "grey.100" : "grey.800",
                bgcolor:
                  message.sender === "user" ? "primary.light" : "grey.100",
                boxShadow: "0 1px 3px rgba(0,0,0,0.1)",
              }}
            >
              <Typography variant="body1" sx={{ wordBreak: "break-word" }}>
                {message.text}
              </Typography>
            </Paper>
            {message.sender === "user" && (
              <Avatar
                sx={{ bgcolor: deepOrange[500], ml: 1, width: 32, height: 32 }}
              >
                U
              </Avatar>
            )}
          </Box>
        ))}
        <div ref={messagesEndRef} />
      </Paper>

      <Box sx={{ display: "flex", alignItems: "center", gap: 2 }}>
        <TextField
          fullWidth
          variant="outlined"
          placeholder="Type your message..."
          value={newMessage}
          onChange={handleInputChange}
          onKeyPress={handleKeyPress}
          sx={{
            "& .MuiOutlinedInput-root": {
              borderRadius: 12,
              bgcolor: "white",
              boxShadow: "0 1px 3px rgba(0,0,0,0.1)",
            },
          }}
          InputProps={{
            endAdornment: (
              <IconButton
                color="primary"
                onClick={handleSendMessage}
                disabled={!newMessage.trim()}
              >
                <SendIcon />
              </IconButton>
            ),
          }}
        />
      </Box>
    </Box>
  );
}

export default ChatView;
