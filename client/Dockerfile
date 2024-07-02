# Use the official Nginx image
FROM nginx:alpine

# Remove the default Nginx configuration file
#RUN rm /etc/nginx/conf.d/default.conf

# Copy your custom configuration file
COPY nginx.conf /etc/nginx/conf.d

# Copy the HTMX frontend files to the Nginx server directory
COPY ./ /usr/share/nginx/html

# Expose port 80
EXPOSE 80 

# Start Nginx server
CMD ["nginx", "-g", "daemon off;"]
