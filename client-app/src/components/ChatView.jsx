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
    { text: "Thank You :)", sender: "user" },
  ]);
  const [newMessage, setNewMessage] = useState("");
  const messagesEndRef = useRef(null);

  const handleSendMessage = () => {
    if (newMessage.trim() !== "") {
      setMessages([...messages, { text: newMessage, sender: "user" }]);
      setNewMessage("");
      // Simulate AI response (replace with actual API call)
      setTimeout(() => {
        setMessages((prevMessages) => [
          ...prevMessages,
          { text: "I'm a simulated AI response!", sender: "ai" },
        ]);
      }, 500);
    }
  };

  const handleInputChange = (event) => {
    setNewMessage(event.target.value);
  };

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
    <Box sx={{ backgroundColor: "#f0f0fa", height: "100vh" }}>
      <Box sx={{ overflowY: "auto", height: "calc(100% - 70px)", pb: 2 }}>
        {messages.map((message, index) => (
          <Box
            key={index}
            sx={{
              display: "flex",
              justifyContent:
                message.sender === "user" ? "flex-end" : "flex-start",
              mb: 1,
            }}
          >
            <Paper
              elevation={3}
              sx={{
                p: 1.5,
                maxWidth: "70%",
                borderRadius: "12px",
                backgroundColor: message.sender === "user" ? "#ffe0b2" : "#fff",
                ml: message.sender === "ai" ? 1 : 0,
                mr: message.sender === "user" ? 1 : 0,
              }}
            >
              <Typography variant="body1">{message.text}</Typography>
            </Paper>
            {message.sender === "ai" && (
              <Avatar
                sx={{
                  bgcolor: lightBlue[500],
                  width: 10,
                  height: 10,
                  mt: 1,
                  ml: 0.5,
                }}
              ></Avatar>
            )}
            {message.sender === "user" && (
              <Avatar
                sx={{
                  bgcolor: deepOrange[500],
                  width: 10,
                  height: 10,
                  mt: 1,
                  mr: 0.5,
                }}
              ></Avatar>
            )}
          </Box>
        ))}
        <div ref={messagesEndRef} />
      </Box>

      {/* Input Area */}
      <Box
        sx={{
          position: "sticky",
          bottom: 0,
          backgroundColor: "#f0f0fa",
          pt: 1,
        }}
      >
        <TextField
          fullWidth
          variant="outlined"
          placeholder="Type a new message here"
          value={newMessage}
          onChange={handleInputChange}
          onKeyPress={handleKeyPress}
          InputProps={{
            endAdornment: (
              <IconButton color="primary" onClick={handleSendMessage}>
                <SendIcon />
              </IconButton>
            ),
            sx: { borderRadius: "24px", backgroundColor: "white" },
          }}
        />
      </Box>
    </Box>
  );
}

export default ChatView;
